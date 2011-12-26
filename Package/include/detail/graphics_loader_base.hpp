#ifndef __KRUSTF_GRAPH_LOADER_BASE_HPP_INCLUDED__
#define __KRUSTF_GRAPH_LOADER_BASE_HPP_INCLUDED__

#include <boost/mpl/bool.hpp>
#include <boost/noncopyable.hpp>
#include <boost/shared_ptr.hpp>
#include <boost/unordered_map.hpp>
#include "graphics_inner.hpp"
#include "safe_bool.hpp"

// グラフィックスの名前空間
namespace krustf { namespace graphics { namespace detail {

// 画像の左右半転に対するフラグ
struct turn_tag {};
struct non_turn_tag {};

template<class T>
struct turn_traits {};
template<>
struct turn_traits<turn_tag> : boost::mpl::true_ {};
template<>
struct turn_traits<non_turn_tag> : boost::mpl::false_ {};

// グラフィックの最小インターフェイス
struct graphic_impl {
	virtual ~graphic_impl() {};
	virtual int width()  =0;
	virtual int height() =0;
	virtual int count()  =0;
	virtual void draw(int,float,float,double,double,bool) =0;
};
typedef boost::shared_ptr<graphic_impl> graphic_impl_t;

/*
グラフィックに対するtraits
ここの特殊化も必要かと思ったがやはり__dxlib_inner__空間関数を書き換えることで済む可能性が高い
*/
template<class T>
struct graph_traits {};
template<>
struct graph_traits<__inner__::handle_type> {
	typedef __inner__::handle_type value_type;

	static void get_size(value_type i,int& w,int& h) {
		__inner__::get_graph_size(i,w,h);
	}
	static int count(value_type) {
		return 1;
	}
	static void draw(value_type handle, int, float x, float y, double scale, double angle, bool turn) {
		__inner__::draw_graph(x,y,scale,angle,handle,true,turn);
	}
	static void delete_graph(value_type handle) {
		__inner__::delete_graph(handle);
	}
};
template<>
struct graph_traits<__inner__::handle_vector_type> {
	typedef __inner__::handle_vector_type value_type;

	static void get_size(value_type const& i,int& w,int& h) {
		__inner__::get_graph_size(i.at(0),w,h);
	}
	static int count(value_type const& i) {
		return static_cast<int>(i.size());
	}
	static void draw(value_type const& i, int index, float x, float y, double scale, double angle, bool turn) {
		__inner__::draw_graph(x,y,scale,angle,i.at(index),true,turn);
	}
	static void delete_graph(value_type const& handle) {
		__inner__::delete_graph(handle);
	}
};

// アニメーションを自動実行しない
struct non_auto_animation {
	int operator()(int index) const {
		return index;
	}
};

// アニメーションを自動実行する
struct auto_animation {
	auto_animation(int interval, int count)
		: Interval_(interval)
		, Count_(count)
		, Index_(0)
		, Frame_(0)
	{
	}

	auto_animation(auto_animation const& other)
		: Interval_(other.Interval_)
		, Count_(other.Count_)
		, Index_(0)
		, Frame_(0)
	{
	}

	int operator()(int /*index*/) const {
		if( (++Frame_ %= Interval_) == 0 ) {
			++Index_ %= Count_;
		}
		return Index_;
	}

private:
	int Interval_;
	int Count_;
	mutable int Index_;
	mutable int Frame_;
};

// implのデフォルト実装
class graphic_base : public graphic_impl {
public:
	virtual ~graphic_base() {};

	int width() {
		return Width_;
	}

	int height() {
		return Height_;
	}

protected:
	int Width_;
	int Height_;
};

template<class HandleT, class IsAutoAnimation = non_auto_animation>
class graphic : public graphic_base {
public:
	typedef graph_traits<HandleT> traits_type;

	graphic(HandleT h, IsAutoAnimation autoAnim = IsAutoAnimation())
		: Handle_(std::forward<HandleT>(h))
		, AutoAnimation_(autoAnim)
	{
		traits_type::get_size(Handle_,Width_,Height_);
	}

	~graphic() {
		traits_type::delete_graph(Handle_);
	}

	int count() {
		return traits_type::count(Handle_);
	}

	void draw(int index, float x, float y, double scale, double angle, bool turn) {
		traits_type::draw(Handle_,AutoAnimation_(index),x,y,scale,angle,turn);
	}

private:
	HandleT Handle_;
	IsAutoAnimation AutoAnimation_;
};

// implを使って描画する
class handle : private safe_bool<handle> {
public:
	using safe_bool_type::operator bool_type;

	handle()
		: Handle_()
	{}

	handle(graphic_impl_t h)
		: Handle_(std::move(h))
	{}

	int width() const {
		Handle_->width();
	}

	int height() const {
		Handle_->height();
	}

	int count() const {
		return Handle_->count();
	}

	template<class TurnT>
	void draw(float x, float y, double scale, double angle, TurnT is_turn)
	{
		typedef turn_traits<TurnT> turn_;
		Handle_->draw(0,x,y,scale,angle,turn_::value);
	}

	template<class TurnT>
	void draw(int index, float x, float y, double scale, double angle, TurnT is_turn)
	{
		typedef turn_traits<TurnT> turn_;
		Handle_->draw(index,x,y,scale,angle,turn_::value);
	}

private:
	bool is_bool_convert() const {
		return Handle_;
	}
	graphic_impl_t Handle_;
};

// 1枚の絵 - 通常の画像であるかチェック
inline
bool is_simple(handle const& i) {
	return i.count() == 1;
}

// 分割された画像であるか
inline
bool is_division(handle const& i) {
	return !is_simple(i);
}

// 上記のhandleをリスト化して管理
class handle_list : private safe_bool<handle_list>
	              , private boost::noncopyable
{
public:
	typedef boost::unordered_map<std::string,graphic_impl_t> list_t;
	typedef handle_list this_type;
	using safe_bool_type::operator bool_type;

	handle_list(list_t && lst)
		: HandleList_(std::move(lst))
	{
	}

	handle_list(this_type && other)
		: HandleList_(std::move(other.HandleList_))
	{
	}

	handle_list()
		: HandleList_()
	{
	}

	this_type & operator=( this_type && other )
	{
		this_type(std::move(other)).swap(*this);
		return *this;
	}

	void swap(this_type & other)
	{
		using std::swap;
		swap(HandleList_,other.HandleList_);
	}

	// 設定したIDで検索
	handle find(std::string const& ID) const
	{
		auto i = HandleList_.find(ID);
		if( i != HandleList_.end() ) {
			return i->second;
		}
		return graphic_impl_t();
	}

	handle operator[](std::string const& ID) const
	{
		auto res = find(ID);
		BOOST_ASSERT(res && "ID does not exist.");
		return res;
	}

private:
	bool is_bool_convert() const {
		return !HandleList_.empty();
	}
	list_t	HandleList_;
};

}; }; }; // namespace krustf::graphics::detail

#endif // __KRUSTF_GRAPH_LOADER_BASE_HPP_INCLUDED__