using SqlSugar;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace RM.Shared.Models
{


    public enum MutLanguageEnum
    {
        中文,
        中文繁体,
        英文,
        日文,
        泰文,
        法文,
        德文
    }
    /// <summary>
    /// 多语言
    /// </summary>
    public class MutLanguage
    {
        /// <summary>
        /// 根据语言环境选择语言
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetLanguage()
        {
            switch (Global.Language)
            {
                case MutLanguageEnum.中文:
                    return zh_CN == null ? "??" : zh_CN;
                case MutLanguageEnum.中文繁体:
                    return zh_TW == null ? "??" : zh_TW;
                case MutLanguageEnum.英文:
                    return en_US == null ? "??" : en_US;
                case MutLanguageEnum.日文:
                    return ja_JP == null ? "??" : ja_JP;
                case MutLanguageEnum.泰文:
                    return th_TH == null ? "??" : th_TH; ;
                case MutLanguageEnum.法文:
                    return fr_FR == null ? "??" : fr_FR; ;
                case MutLanguageEnum.德文:
                    return zh_CN == null ? "??" : zh_CN;
                default:
                    break;
            }
            return "??";
        }


        /// <summary>
        /// 根据语言环境选择语言
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string this[MutLanguageEnum index]
        {
            get
            {
                switch (index)
                {
                    case MutLanguageEnum.中文:
                        return zh_CN == null ? "??" : zh_CN;
                    case MutLanguageEnum.中文繁体:
                        return zh_TW == null ? "??" : zh_TW;
                    case MutLanguageEnum.英文:
                        return en_US == null ? "??" : en_US;
                    case MutLanguageEnum.日文:
                        return ja_JP == null ? "??" : ja_JP;
                    case MutLanguageEnum.泰文:
                        return th_TH == null ? "??" : th_TH; ;
                    case MutLanguageEnum.法文:
                        return fr_FR == null ? "??" : fr_FR; ;
                    case MutLanguageEnum.德文:
                        return zh_CN == null ? "??" : zh_CN;
                    default:
                        break;
                }
                return "??";
            }
            set { /* set the specified index to value here */ }
        }
        /// <summary>
        /// 中文(大陆)
        /// </summary>
        [SugarColumn(Length = 150)]
        [DisplayName("中文")]
        public string? zh_CN { get; set; }

        /// <summary>
        /// 中文(台湾),一般表示繁体
        /// </summary>
        [DisplayName("中文_繁体")]
        [SugarColumn(Length = 150, IsNullable = true)]
        public string? zh_TW { get; set; }

        /// <summary>
        /// 英文 美国
        /// </summary>
        [DisplayName("英文")]
        [SugarColumn(Length = 150)]
        public string? en_US { get; set; }

        /// <summary>
        /// 日本
        /// </summary>
        [DisplayName("日文")]
        [SugarColumn(Length = 150, IsNullable = true)]
        public string? ja_JP { get; set; }

        /// <summary>
        /// 泰国
        /// </summary> 
        [DisplayName("泰文")]
        [SugarColumn(Length = 150, IsNullable = true)]
        public string? th_TH { get; set; }

        /// <summary>
        /// 法国
        /// </summary>
        [SugarColumn(Length = 150, IsNullable = true)]
        [DisplayName("法文")]
        public string? fr_FR { get; set; }
    }
}
