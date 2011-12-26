#ifndef __KRUSTF_GRAPHICS_INNER_HPP_INCLUDED__
#define __KRUSTF_GRAPHICS_INNER_HPP_INCLUDED__

#include <DxLib.h>
#include <vector>
#include <string>
#include <boost/range/algorithm/for_each.hpp>

/*
	DxLibの名前の関数を呼び出すところ
	同じ名前、同じ実装があればDxLib以外でも置き換えられるはず
	handle_typeやhandle_vector_typeの要求をクリアする必要がある
*/
namespace __inner_dxlib__ {

/*
	グラフィックハンドルの最小単位
	DxLibにおけるグラフのID
	is copyable objects.
*/
typedef int handle_type;

/*
	グラフィックハンドルの配列を示す
	is movable objects.
*/
typedef std::vector<handle_type> handle_vector_type;

// グラフィックを描画する.
inline
void draw_graph(float x, float y, double exRate, double angle, int handle, bool trans, bool turn)
{
	::DxLib::DrawRotaGraphF(x,y,exRate,angle,handle,trans,turn);
}

// 通常のグラフィックをロード.
inline
handle_type load_graph(std::string const& path)
{
	return ::DxLib::LoadGraph(path.c_str());
}

// 分割グラフィックのロード.
inline
handle_vector_type load_divgraph(std::string const& path, int x, int y, int all, int w, int h)
{
	handle_vector_type v(all,0);
	::DxLib::LoadDivGraph(path.c_str(),all,x,y,w,h,v.data());
	return std::move(v);
}

// グラフィック1つに対するサイズを取得
inline
void get_graph_size(handle_type i, int& w, int& h)
{
	::DxLib::GetGraphSize(i,&w,&h);
}

// グラフィックを削除
inline
void delete_graph(handle_type h)
{
	::DxLib::DeleteGraph(h);
}

// 複数グラフィックのRange用
inline
void delete_graph(handle_vector_type const& h)
{
	for(auto i = std::begin(h); i != std::end(h); ++i) {
		delete_graph(*i);
	}
}

}; // namespace __inner_dxlib__

/*
	置換用
	krustf::graphics::detailヘッダ内ではここを参照する
*/
namespace __inner__ {
	using namespace __inner_dxlib__;
}; // namespace __inner__

#endif // __KRUSTF_GRAPHICS_INNER_HPP_INCLUDED__