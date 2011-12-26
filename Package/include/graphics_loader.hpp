#ifndef __KRUSTF_GRAPH_LOADER_HPP_INCLUDED__
#define __KRUSTF_GRAPH_LOADER_HPP_INCLUDED__

/* --------------------------
	graphics_loader.hpp - krustf

	[概要]
	AnimationCreator(仮)で作られた画像リスト郡を
	C++で読み込むライブラリ

	[推奨環境] - 作成環境と同義
	Boost C++ Libraries 1.47.0
	DxLib ver 3.06c

	[免責]
	当ライブラリを利用した際におけるトラブル等に対し
	作者は一切責任を負いません

	[bug fix and updates]
	2011.11.09
	  - 複数ソースファイルでの利用時に多重定義エラーになるのを修正
	2011.11.20
	  - Safe Bool Idiomをクラステンプレート及び別ヘッダに切り離し
	  - 等ヘッダ内では利用可能なクラス、関数についてのみusingで表記
*/
#include "detail/graphics_loader_base.hpp"
#include "detail/parse_create.hpp"

namespace krustf { namespace graphics {

// 画像の左右反転フラグ
static const detail::turn_tag     turn;
static const detail::non_turn_tag non_turn;

using detail::handle;		// グラフィックのハンドル
using detail::handle_list;	// ハンドルのリスト　通常はこれから検索
using detail::load;			// リストを生成したxmlファイルから作成して返す

using detail::is_simple;	// 通常の1枚絵であるか
using detail::is_division;	// 分割された画像であるか

}; }; // namespace krustf::graphics

#endif // __KRUSTF_GRAPH_LOADER_HPP_INCLUDED__