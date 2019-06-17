using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SmeshlinkProject.Utils
{
    class UIUtil
    {
        /// <summary>
        /// 设置窗口标题和背景样式
        /// </summary>
        /// <param name="accentName">窗口标题栏样式</param>
        /// <param name="themeName">背景样式</param>
        public static void ChangeTheme(string accentName, string themeName)
        {
            //this.ShowMessageAsync("标题", "内容", MessageDialogStyle.Affirmative, new MetroDialogSettings() { AffirmativeButtonText = "确定" });
            Accent expectedAccent = ThemeManager.GetAccent(accentName); //“Red”, “Green”, “Blue”, “Purple”, “Orange”, “Lime”, “Emerald”, “Teal”, “Cyan”, “Cobalt”, “Indigo”, “Violet”, “Pink”, “Magenta”, “Crimson”, “Amber”, “Yellow”, “Brown”, “Olive”, “Steel”, “Mauve”, “Taupe”, “Sienna”
            AppTheme expectedTheme = ThemeManager.GetAppTheme(themeName);  //"BaseDark"“BaseLight”
            ThemeManager.ChangeAppStyle(Application.Current, expectedAccent, expectedTheme);
        }
    }
}
