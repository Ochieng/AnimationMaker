#ifndef __KRUSTF_SAFE_BOOL_IDIOM_HPP_INCLUDED__
#define __KRUSTF_SAFE_BOOL_IDIOM_HPP_INCLUDED__

/* ------------------------
	safe_bool.hpp - krustf

	Safe Bool Idiom�̔ėp����

	bool is_bool_convert() const
	��L�̃��\�b�h���ł��̃N���X�̐^�U�l�����肵�܂�.

	[����]
	�@�K��private�p�����Apublic�X�R�[�v����
	�@using safe_bool_type::operator bool_type;
	�@���s���Ă�������.
	�@�s�v�ȕϊ��ɂ��폜�Ȃǃo�O�����炷���߂̏��u�ł�.
*/

namespace krustf { namespace detail {

struct safe_bool_base {
	typedef void (safe_bool_base::*bool_type)() const;
	void this_type_does_not_support_comparisons() const {};

protected:
	safe_bool_base() {};
	safe_bool_base(safe_bool_base const&) {};
	safe_bool_base& operator=(safe_bool_base const&) { return *this; };
};

// CRTP need of 'is_bool_convert' method.
template<class T = void>
struct safe_bool : private safe_bool_base {
	typedef safe_bool safe_bool_type;

	operator bool_type() const {
		return static_cast<T const*>(this)->is_bool_convert()
			? &safe_bool_base::this_type_does_not_support_comparisons : 0;
	}

protected:
	safe_bool() {};
};

// pure virtual to 'is_bool_convert' method.
template<>
struct safe_bool<void> : private safe_bool_base {
	typedef safe_bool safe_bool_type;

	operator bool_type() const {
		return is_bool_convert()
			? &safe_bool_base::this_type_does_not_support_comparisons : 0;
	}

protected:
	safe_bool() {};

private:
	virtual bool is_bool_convert() const =0;
};

}; // namespace detail

using detail::safe_bool;

}; // namespace krustf

#endif // __KRUSTF_SAFE_BOOL_IDIOM_HPP_INCLUDED__