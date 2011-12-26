#ifndef __KRUSTF_GRAPHICS_INNER_HPP_INCLUDED__
#define __KRUSTF_GRAPHICS_INNER_HPP_INCLUDED__

#include <DxLib.h>
#include <vector>
#include <string>
#include <boost/range/algorithm/for_each.hpp>

/*
	DxLib�̖��O�̊֐����Ăяo���Ƃ���
	�������O�A���������������DxLib�ȊO�ł��u����������͂�
	handle_type��handle_vector_type�̗v�����N���A����K�v������
*/
namespace __inner_dxlib__ {

/*
	�O���t�B�b�N�n���h���̍ŏ��P��
	DxLib�ɂ�����O���t��ID
	is copyable objects.
*/
typedef int handle_type;

/*
	�O���t�B�b�N�n���h���̔z�������
	is movable objects.
*/
typedef std::vector<handle_type> handle_vector_type;

// �O���t�B�b�N��`�悷��.
inline
void draw_graph(float x, float y, double exRate, double angle, int handle, bool trans, bool turn)
{
	::DxLib::DrawRotaGraphF(x,y,exRate,angle,handle,trans,turn);
}

// �ʏ�̃O���t�B�b�N�����[�h.
inline
handle_type load_graph(std::string const& path)
{
	return ::DxLib::LoadGraph(path.c_str());
}

// �����O���t�B�b�N�̃��[�h.
inline
handle_vector_type load_divgraph(std::string const& path, int x, int y, int all, int w, int h)
{
	handle_vector_type v(all,0);
	::DxLib::LoadDivGraph(path.c_str(),all,x,y,w,h,v.data());
	return std::move(v);
}

// �O���t�B�b�N1�ɑ΂���T�C�Y���擾
inline
void get_graph_size(handle_type i, int& w, int& h)
{
	::DxLib::GetGraphSize(i,&w,&h);
}

// �O���t�B�b�N���폜
inline
void delete_graph(handle_type h)
{
	::DxLib::DeleteGraph(h);
}

// �����O���t�B�b�N��Range�p
inline
void delete_graph(handle_vector_type const& h)
{
	for(auto i = std::begin(h); i != std::end(h); ++i) {
		delete_graph(*i);
	}
}

}; // namespace __inner_dxlib__

/*
	�u���p
	krustf::graphics::detail�w�b�_���ł͂������Q�Ƃ���
*/
namespace __inner__ {
	using namespace __inner_dxlib__;
}; // namespace __inner__

#endif // __KRUSTF_GRAPHICS_INNER_HPP_INCLUDED__