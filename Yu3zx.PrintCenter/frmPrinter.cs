﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Yu3zx.PrintCenter
{
    public partial class frmPrinter : Form
    {
        public frmPrinter()
        {
            InitializeComponent();
        }

        private void frmPrinter_Load(object sender, EventArgs e)
        {
            //获取打印机
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cboInitprinter.Items.Add(printer);
            }
            cboInitprinter.SelectedIndex = 0;
            Init();
        }

        public void Init()
        {
            if (File.Exists("Config/ItemConfig.xml"))
            {
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load("Config/ItemConfig.xml");

                    //PrintConfig 30*25
                    XmlNode printNode = xmlDoc.SelectSingleNode("Configuration/PrintConfig"); //
                    foreach (XmlNode nodeSub in printNode.ChildNodes)
                    {
                        string sKey = nodeSub.Attributes["dataname"].Value.Trim();
                        string sMatch = nodeSub.Attributes["matchname"].Value.Trim();
                        if (!PrintHelper.TempleteFieldsList.ContainsKey(sKey))
                        {
                            PrintHelper.TempleteFieldsList.Add(sKey, sMatch);
                        }
                    }
                    //FabricConfig 布卷标签
                    XmlNode fabricNode = xmlDoc.SelectSingleNode("Configuration/FabricConfig"); //
                    foreach (XmlNode nodeSub in fabricNode.ChildNodes)
                    {
                        string sKey = nodeSub.Attributes["dataname"].Value.Trim();
                        string sMatch = nodeSub.Attributes["matchname"].Value.Trim();
                        if (!PrintHelper.FabricTempleteFieldsList.ContainsKey(sKey))
                        {
                            PrintHelper.FabricTempleteFieldsList.Add(sKey, sMatch);
                        }
                    }
                    //CartonConfig 箱外标签
                    XmlNode cartonNode = xmlDoc.SelectSingleNode("Configuration/CartonConfig"); //
                    foreach (XmlNode nodeSub in cartonNode.ChildNodes)
                    {
                        string sKey = nodeSub.Attributes["dataname"].Value.Trim();
                        string sMatch = nodeSub.Attributes["matchname"].Value.Trim();
                        if (!PrintHelper.CartonTempleteFieldsList.ContainsKey(sKey))
                        {
                            PrintHelper.CartonTempleteFieldsList.Add(sKey, sMatch);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("文件不存在!");
            }
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            FabricClothItem clothItem = new FabricClothItem();
            clothItem.BatchNo = txtItemBN.Text;
            clothItem.ProduceNum = float.Parse(txtItemPN.Text);
            clothItem.ReelNum = int.Parse(txtItemSN.Text);
            Dictionary<string, string> dictData = PrintHelper.GetEntityPropertyToDict(clothItem);
            string lblFile = Application.StartupPath + "\\Templates\\" + "labCloth.btw";
            if (File.Exists(lblFile))
            {
                PrintHelper.CreateInstance().BarPrintInit(lblFile, cboInitprinter.Text, dictData, PrintHelper.TempleteFieldsList, (int)nudCopys.Value);
            }
        }

        private void btnFabric_Click(object sender, EventArgs e)
        {
            FabricClothItem clothItem = new FabricClothItem();
            clothItem.BatchNo = txtBatchNo.Text;
            clothItem.ColorNum = txtColorNum.Text;
            clothItem.ProduceNum = float.Parse(txtProduceNum.Text);
            clothItem.Specs = txtSpecs.Text;
            clothItem.QualityString = txtQuatilyString.Text;
            clothItem.ReelNum = int.Parse(txtFabItemNum.Text); //
            clothItem.QualityName = txtQName.Text;
            Dictionary<string, string> dictData = PrintHelper.GetEntityPropertyToDict(clothItem);

            string lblFile = string.Empty;
            if (clothItem.QualityName != "A")
            {
                lblFile = Application.StartupPath + "\\Templates\\KcSc" + "FabricLbl.btw";
            }
            else
            {
                lblFile = Application.StartupPath + "\\Templates\\" + "FabricLbl.btw";
            }

            if (File.Exists(lblFile))
            {
                PrintHelper.CreateInstance().BarPrintInit(lblFile, cboInitprinter.Text, dictData, PrintHelper.FabricTempleteFieldsList, (int)nudCopys.Value);
            }
        }

        private void btnCarton_Click(object sender, EventArgs e)
        {
            CartonBoxLabel carton = new CartonBoxLabel();
            carton.BatchNo = txtBoxBn.Text;
            carton.BoxNum = txtBoxNum.Text;
            carton.ColorNum = txtBoxColor.Text;
            carton.Specs = txtBoxSpecs.Text;
            carton.QualityString = txtBoxQs.Text;

            carton.ReelNum1 = int.Parse(txtSerial1.Text);
            carton.ReelNum2 = int.Parse(txtSerial2.Text);
            carton.ReelNum3 = int.Parse(txtSerial3.Text);
            carton.ReelNum4 = int.Parse(txtSerial4.Text);
            carton.ReelNum5 = int.Parse(txtSerial5.Text);
            carton.ReelNum6 = int.Parse(txtSerial6.Text);

            carton.RollNum1 = decimal.Parse(txtRoll1.Text);
            carton.RollNum2 = decimal.Parse(txtRoll2.Text);
            carton.RollNum3 = decimal.Parse(txtRoll3.Text);
            carton.RollNum4 = decimal.Parse(txtRoll4.Text);
            carton.RollNum5 = decimal.Parse(txtRoll5.Text);
            carton.RollNum6 = decimal.Parse(txtRoll6.Text);

            Dictionary<string, string> dictData = PrintHelper.GetEntityPropertyToDict(carton);
            string lblFile = Application.StartupPath + "\\Templates\\" + "FabricCarton.btw";
            if (File.Exists(lblFile))
            {
                PrintHelper.CreateInstance().BarPrintInit(lblFile, cboInitprinter.Text, dictData, PrintHelper.CartonTempleteFieldsList, (int)nudCopys.Value);
            }
        }
    }
}
