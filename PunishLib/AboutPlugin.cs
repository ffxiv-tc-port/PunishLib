using Dalamud.Interface;
using Dalamud.Interface.Colors;
using Dalamud.Bindings.ImGui;
using PunishLib.ImGuiMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PunishLib
{
    public class AboutPlugin
    {
        public string Sponsor { get; set; }
        public string Developer { get; set; }

        /// <summary>
        /// 關於分頁圖示的本地檔路徑；不設（null）時沿用 manifest 的 IconUrl。
        /// 外掛庫是私有 repo 時，那個 raw 網址不帶 token 一律 404，圖示畫不出來。
        /// </summary>
        public string IconPath { get; set; }

        internal Action WithLoveBy => Developer == null ? WithLoveByPunish : WithLoveByOther;

        static void WithLoveByPunish()
        {
            ImGuiEx.ImGuiLineCentered("AboutHeader", delegate
            {
                ImGuiEx.Text($"Published and developed with ");
                ImGui.PushFont(UiBuilder.IconFont);
                ImGui.SameLine(0, 0);
                ImGuiEx.Text(ImGuiColors.DalamudRed, FontAwesomeIcon.Heart.ToIconString());
                ImGui.PopFont();
                ImGui.SameLine(0, 0);
                ImGuiEx.Text($" by Puni.sh");
            });
        }

        static void WithLoveByOther()
        {
            //Developed with ❤️ by {Developer}, published by Puni.sh
            ImGuiEx.ImGuiLineCentered("AboutHeader", delegate
            {
                ImGuiEx.Text($"Developed with ");
                ImGui.PushFont(UiBuilder.IconFont);
                ImGui.SameLine(0, 0);
                ImGuiEx.Text(ImGuiColors.DalamudRed, FontAwesomeIcon.Heart.ToIconString());
                ImGui.PopFont();
                ImGui.SameLine(0, 0);
                ImGuiEx.Text($" by {PunishLibMain.About.Developer}, published by Puni.sh");
            });
        }
    }

}
