#region Namespaces
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
#endregion

namespace APIViet.Ribbon
{
    public class CustomPushButton
    {
        public CustomPushButton() {}
       
        //PushButton
        
        public static PushButton NewButton(RibbonPanel panel, string btnName,string btnText, string assemblyName, string className, string largeImageName ="", string btnTooltip ="")
        {
            try
            {
                PushButtonData btnData = new PushButtonData(btnName, btnText, assemblyName, className);

                btnData.ToolTip = btnTooltip;
                ContextualHelp help = new ContextualHelp(ContextualHelpType.Url, "https://help.autodesk.com");
                btnData.SetContextualHelp(help);
                btnData.LargeImage = Image.ImageSource(largeImageName);
  
                //Add buton to panel
                PushButton btn = panel.AddItem(btnData) as PushButton;
                return btn;
            }
            catch(Exception)
            {
                return null;
                throw;
            }
        }
 
    }
}