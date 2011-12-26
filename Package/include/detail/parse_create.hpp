#ifndef __KRUSTF_GRAPH_PARSE_CREATE_HPP_INCLUDED__
#define __KRUSTF_GRAPH_PARSE_CREATE_HPP_INCLUDED__

#include <string>
#include <boost/unordered_map.hpp>
#include <boost/shared_ptr.hpp>
#include <boost/make_shared.hpp>
#include <boost/property_tree/xml_parser.hpp>
#include <boost/property_tree/ptree.hpp>
#include <boost/range/algorithm/for_each.hpp>
#include "graphics_inner.hpp"
#include "graphics_loader_base.hpp"

namespace krustf { namespace graphics { namespace detail {

// 通常のグラフィック
inline
graphic_impl_t create_graph(std::string const& path)
{
	using namespace __inner__;
	typedef graphic<handle_type,non_auto_animation> simple_graphic_type;
	return boost::make_shared<simple_graphic_type>(load_graph(path));
}

// 分割されたグラフィック
inline
graphic_impl_t create_graph(std::string const& path, int x, int y, int all, int w, int h)
{
	using namespace __inner__;
	typedef graphic<__inner__::handle_vector_type,non_auto_animation> div_graphic_type;
	return boost::make_shared<div_graphic_type>(load_divgraph(path,x,y,all,w,h));
}

// アニメーション
inline
graphic_impl_t create_graph(std::string const& path, int interval, int x, int y, int all, int w, int h)
{
	using namespace __inner__;
	typedef graphic<handle_vector_type,auto_animation> animation_type;
	return boost::make_shared<animation_type>(load_divgraph(path,x,y,all,w,h),auto_animation(interval,all));
}

using namespace boost::property_tree;

inline
ptree get_graphics_node(ptree const& pt) {
	return pt.get_child("graphics");
}

inline
std::string get_directory(ptree const& pt) {
	return pt.get<std::string>("<xmlattr>.directory");
}

inline
std::string get_path(ptree const& pt) {
	return pt.get_value<std::string>();
}

inline
std::string get_id(ptree const& pt) {
	return pt.get<std::string>("<xmlattr>.id");
}

inline
int get_x(ptree const& pt) {
	return pt.get<int>("<xmlattr>.x");
}

inline
int get_y(ptree const& pt) {
	return pt.get<int>("<xmlattr>.y");
}

inline
int get_all(ptree const& pt) {
	return pt.get<int>("<xmlattr>.all");
}

inline
int get_width(ptree const& pt) {
	return pt.get<int>("<xmlattr>.width");
}

inline
int get_height(ptree const& pt) {
	return pt.get<int>("<xmlattr>.height");
}

inline
int get_interval(ptree const& pt) {
	return pt.get<int>("<xmlattr>.interval");
}

inline
bool is_simple_node(std::string const& i) {
	return i == "entry";
}

inline
bool is_div_node(std::string const& i) {
	return i == "diventry";
}

inline
bool is_animation_node(std::string const& i) {
	return i == "animation";
}

inline
void insert_simple(ptree const& pt, std::string const& dir, handle_list::list_t & lst)
{
	// 既にentryノードへ移動済み
	const std::string id   = get_id(pt);
	const std::string path = get_path(pt);
	lst.insert(std::make_pair(id,create_graph(dir + path)));
}

inline
void insert_division(ptree const& pt, std::string const& dir, handle_list::list_t & lst)
{
	// 既にdiventryノードへ移動済み
	const std::string id   = get_id(pt);
	const int         x    = get_x(pt);
	const int         y    = get_y(pt);
	const int         all  = get_all(pt);
	const int         w    = get_width(pt);
	const int         h    = get_height(pt);
	const std::string path = get_path(pt);
	lst.insert(std::make_pair(id,create_graph(dir + path,x,y,all,w,h)));
}

inline
void insert_animation(ptree const& pt, std::string const& dir, handle_list::list_t & lst)
{
	// 既にanimationノードへ移動済み
	const std::string id   = get_id(pt);
	const int         itvl = get_interval(pt);
	const int         x    = get_x(pt);
	const int         y    = get_y(pt);
	const int         all  = get_all(pt);
	const int         w    = get_width(pt);
	const int         h    = get_height(pt);
	const std::string path = get_path(pt);
	lst.insert(std::make_pair(id,create_graph(dir + path,itvl,x,y,all,w,h)));
}

// graph_loader の作成
inline
handle_list load(std::string const& xmlFilePath)
{
	ptree pt;
	read_xml(xmlFilePath,pt);
	handle_list::list_t lst;

	if(!pt.empty()) {
		ptree subpt = get_graphics_node(pt);
		const std::string dir = get_directory(subpt);
		auto push_handles =
			[&](ptree::const_iterator::value_type const& i) {
				if(is_simple_node(i.first))
					insert_simple(i.second,dir,lst);
				else if(is_div_node(i.first))
					insert_division(i.second,dir,lst);
				else if(is_animation_node(i.first))
					insert_animation(i.second,dir,lst);
				else
					BOOST_ASSERT("internal error. - node name no matching." && 0);
			};
		boost::for_each(subpt,push_handles);
	}
	return handle_list(std::move(lst));
}

}; }; }; // krustf::graphics::detail

#endif // __KRUSTF_GRAPH_PARSE_CREATE_HPP_INCLUDED__