using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.FactoryLine
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        private int showtele = 0;
        private int haverec = 0;

        public MainWindowModel()
        {
            //-=-=-=-=-=-=-=-=-=-=-=-=-=-
            MyMenu main = new MyMenu();
            main.HeaderName = "生产计划";
            main.IsChecked = true;
            main.SubMenus = new List<MyMenu>();

            MyMenu sub1 = BuildMenu("排产计划",true, "ProductPlanPage.xaml");
            main.SubMenus.Add(sub1);

            //MyMenu sub2 = BuildMenu("检验布料", false, "BeOnlinePage.xaml");
            //main.SubMenus.Add(sub2);

            //MyMenu sub3 = BuildMenu("产品入库", false, "ProductIncomingPage.xaml");
            //main.SubMenus.Add(sub3);

            //MyMenu sub4 = BuildMenu("产品出库", false, "ProductDeliveryPage.xaml");
            //main.SubMenus.Add(sub4);

            MenuLists.Add(main);
            //-=-=-=-=-=-=-=-=-=-=-=-=-=-
            MyMenu main1 = new MyMenu();
            main1.HeaderName = "数据查询";
            main1.IsChecked = false;
            main1.SubMenus = new List<MyMenu>();

            MyMenu sub11 = BuildMenu("计划查询", true, "ProductPlanSearchPage.xaml");
            main1.SubMenus.Add(sub11);

            MyMenu sub21 = BuildMenu("布卷查询", false, "FabricClothSearchPage.xaml");
            main1.SubMenus.Add(sub21);

            //MyMenu sub31 = BuildMenu("装箱查询", false, "CartonInfoSearchPage.xaml");
            //main1.SubMenus.Add(sub31);

            //MyMenu sub41 = BuildMenu("产品出库", false, "ProductDeliveryPage.xaml");
            //main.SubMenus.Add(sub41);

            MenuLists.Add(main1);
        }
        /// <summary>
        /// 生成菜单
        /// </summary>
        /// <param name="hname"></param>
        /// <param name="chked"></param>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        private MyMenu BuildMenu(string hname,bool chked,string strUrl)
        {
            MyMenu menu = new MyMenu();
            menu.HeaderName = hname;
            menu.IsChecked = chked;
            menu.MenuUrl = strUrl;
            return menu;
        }

        #region 属性

        public List<MyMenu> MenuLists = new List<MyMenu>();

        public int ShowTele
        {
            get
            {
                return showtele;
            }
            set
            {
                showtele = value;
                NotifyPropertyChange("ShowTele");
            }
        }

        public int ComeOut
        {
            get
            {
                return haverec;
            }
            set
            {
                haverec = value;
                NotifyPropertyChange("ComeOut");
            }
        }

        #endregion End


        #region 属性监听接口
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                //根据PropertyChanged事件的委托类，实现PropertyChanged事件：
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion End
    }

    public class OrgModel
    {
        public bool IsGrouping { get; set; }
        public ObservableCollection<OrgModel> Children { get; set; }
        public string DisplayName { get; set; }
        public string SurName { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public int Count { get; set; }
    }

    public class MyMenu
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string HeaderName { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsChecked { get; set; }
        /// <summary>
        /// 跳转地址
        /// </summary>
        public string MenuUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 图标地址
        /// </summary>
        public string Icon
        {
            get;
            set;
        }

        public List<MyMenu> SubMenus
        {
            get;
            set;
        }
    }
}
