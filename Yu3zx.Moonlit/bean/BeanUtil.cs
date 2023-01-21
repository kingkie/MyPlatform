using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yu3zx.Moonlit.service;

namespace Yu3zx.Moonlit.bean
{
    //bean的单例模式便于获取只生成一次
    public class BeanUtil
    {
        private static SysProductInfoService sysProductInfoService = null;
        private static SysBaseInfoService sysBaseInfoService = null;
        private static SysPlcInfoService sysPlcInfoService = null;
        private static YyCollectInfoBakService yyCollectInfoBakService = null;
        private static YyCollectInfoService yyCollectInfoService = null;
        private static YyOpSwitchService yyOpSwitchService = null;
        private static YyQrcodeInfoService yyQrcodeInfoService = null;
        private static SysProductPartsService sysProductPartsService = null;
        public static BaseService getBean(string bean) {
            if ("sysProductInfoService".Equals(bean)) {
                if (sysProductInfoService==null) {
                    sysProductInfoService = new SysProductInfoService();
                }
                return sysProductInfoService;
            }

            if ("sysBaseInfoService".Equals(bean))
            {
                if (sysBaseInfoService == null)
                {
                    sysBaseInfoService = new SysBaseInfoService();
                }
                return sysBaseInfoService;
            }

            if ("sysPlcInfoService".Equals(bean))
            {
                if (sysPlcInfoService == null)
                {
                    sysPlcInfoService = new SysPlcInfoService();
                }
                return sysPlcInfoService;
            }

            if ("yyCollectInfoBakService".Equals(bean))
            {
                if (yyCollectInfoBakService == null)
                {
                    yyCollectInfoBakService = new YyCollectInfoBakService();
                }
                return yyCollectInfoBakService;
            }
            if ("yyCollectInfoService".Equals(bean))
            {
                if (yyCollectInfoService == null)
                {
                    yyCollectInfoService = new YyCollectInfoService();
                }
                return yyCollectInfoService;
            }


            if ("yyOpSwitchService".Equals(bean))
            {
                if (yyOpSwitchService == null)
                {
                    yyOpSwitchService = new YyOpSwitchService();
                }
                return yyOpSwitchService;
            }
            if ("yyQrcodeInfoService".Equals(bean))
            {
                if (yyQrcodeInfoService == null)
                {
                    yyQrcodeInfoService = new YyQrcodeInfoService();
                }
                return yyQrcodeInfoService;
            }
            if ("sysProductPartsService".Equals(bean))
            {
                if (sysProductPartsService == null)
                {
                    sysProductPartsService = new SysProductPartsService();
                }
                return sysProductPartsService;
            }
            
            return null;
        }
    }
}
