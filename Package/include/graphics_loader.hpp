#ifndef __KRUSTF_GRAPH_LOADER_HPP_INCLUDED__
#define __KRUSTF_GRAPH_LOADER_HPP_INCLUDED__

/* --------------------------
	graphics_loader.hpp - krustf

	[�T�v]
	AnimationCreator(��)�ō��ꂽ�摜���X�g�S��
	C++�œǂݍ��ރ��C�u����

	[������] - �쐬���Ɠ��`
	Boost C++ Libraries 1.47.0
	DxLib ver 3.06c

	[�Ɛ�]
	�����C�u�����𗘗p�����ۂɂ�����g���u�����ɑ΂�
	��҂͈�ؐӔC�𕉂��܂���

	[bug fix and updates]
	2011.11.09
	  - �����\�[�X�t�@�C���ł̗��p���ɑ��d��`�G���[�ɂȂ�̂��C��
	2011.11.20
	  - Safe Bool Idiom���N���X�e���v���[�g�y�ѕʃw�b�_�ɐ؂藣��
	  - ���w�b�_���ł͗��p�\�ȃN���X�A�֐��ɂ��Ă̂�using�ŕ\�L
*/
#include "detail/graphics_loader_base.hpp"
#include "detail/parse_create.hpp"

namespace krustf { namespace graphics {

// �摜�̍��E���]�t���O
static const detail::turn_tag     turn;
static const detail::non_turn_tag non_turn;

using detail::handle;		// �O���t�B�b�N�̃n���h��
using detail::handle_list;	// �n���h���̃��X�g�@�ʏ�͂��ꂩ�猟��
using detail::load;			// ���X�g�𐶐�����xml�t�@�C������쐬���ĕԂ�

using detail::is_simple;	// �ʏ��1���G�ł��邩
using detail::is_division;	// �������ꂽ�摜�ł��邩

}; }; // namespace krustf::graphics

#endif // __KRUSTF_GRAPH_LOADER_HPP_INCLUDED__