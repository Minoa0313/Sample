using System;
using System.Windows;

namespace WpfControlLibrary1
{
    public interface IWindowInfo
    {
        public bool LanguageSwitch(bool isEnglish);
    }

    public class Class1:IWindowInfo
    {
        private UserControl1 control;

        public Class1(UserControl1 obj)
        {
            control = obj;
        }

        public bool LanguageSwitch(bool isEnglish)
        {
            ResourceDictionary? langRd = null;
            string langFile = "";

            if (isEnglish)
            {
                langFile = "lang/en.xaml";
            }
            else
            {
                langFile = "lang/jp.xaml";
            }

            try
            {
                // ここは大事！
                // DLL から リソースディレクトリを使うとき、
                // Uri の書き方 new Uri("/現在のプロジェクト名;component/相対パス",UriKind.Relative)
                langRd = Application.LoadComponent(new Uri("/WpfControlLibrary1;component/" + langFile, UriKind.Relative)) as ResourceDictionary;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            if (langRd != null)
            {
                if (control.Resources.MergedDictionaries.Count > 0)
                {
                    // 設計画面で言語リソースディレクトリを0番に固定で設定したので
                    control.Resources.MergedDictionaries[0] = langRd;
                }
                else
                {
                    control.Resources.MergedDictionaries.Add(langRd);
                }
            }

            return true;
        }
    }
}
