#include <DxLib.h>
#include <stdexcept>
#include "graphics_loader.hpp"

struct DxLibInitializer {
	DxLibInitializer() {
		SetOutApplicationLogValidFlag(FALSE);
		ChangeWindowMode(TRUE);
		SetDrawScreen(DX_SCREEN_BACK);
		SetGraphMode(320,240,32,60);
		if( DxLib_Init() < 0 ) {
			throw std::runtime_error("internal error, DxLib initialize failed.");
		}
	}

	~DxLibInitializer() {
		DxLib_End();
	}
};

struct non_virtual_test : public krustf::safe_bool<non_virtual_test> {
	bool is_bool_convert() const {
		return true;
	}
};

struct virtual_test : public krustf::safe_bool<> {
	bool is_bool_convert() const {
		return true;
	}
};

int WINAPI WinMain(HINSTANCE,HINSTANCE,LPSTR,int)
{
	namespace kg = krustf::graphics;
	DxLibInitializer _Init;

	// Load XML.
	kg::handle_list list = kg::load("graphics.xml");
	BOOST_ASSERT(list);

	kg::handle handle    = list.find("simple1");
	kg::handle divhandle = list["animation1"];

	BOOST_ASSERT(handle);
	BOOST_ASSERT(divhandle);

	BOOST_ASSERT(kg::is_division(handle) != kg::is_simple(handle));

	while(!ProcessMessage() && !CheckHitKey(KEY_INPUT_ESCAPE)) {
		ClearDrawScreen();
		handle.draw(80, 120, 1.0, 0.0, kg::non_turn);
		divhandle.draw(240, 120, 1.0, 0.0, kg::non_turn);
		ScreenFlip();
	}
	
	return 0;
}