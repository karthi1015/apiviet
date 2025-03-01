#region Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Windows;
using UIFramework;
#endregion

namespace ApiViet.Ribbon
{
    public class CustomTab
    {
        private readonly CustomRibbon _ribbon;
        private readonly Autodesk.Revit.UI.Tab? _systemTab;
        private readonly string _name;



        public CustomTab(CustomRibbon ribbon, string name)
        {
            _ribbon = ribbon;
            _name = name;
        }

        public CustomTab(CustomRibbon ribbon, Autodesk.Revit.UI.Tab systemTab)
        {
            _ribbon = ribbon;
            _systemTab = systemTab;
        }

        internal CustomRibbon Ribbon => _ribbon;
        public CustomPanel Panel(string panelTitle)
        {
            //Search if panel existed in this tab
            List<Autodesk.Revit.UI.RibbonPanel> panels;
            panels = _systemTab is null
                ? _ribbon.Application.GetRibbonPanels(_name)
                : _ribbon.Application.GetRibbonPanels(_systemTab.Value);
            ;

            foreach (var panel in panels.Where(panel => panel.Name.Equals(panelTitle)))
            {
                panel.AddSeparator();
                return new CustomPanel(this, panel);
            }

            //Search if panel existed in other custom tab


            //Create new panel
            Autodesk.Revit.UI.RibbonPanel ribbonPanel;
            ribbonPanel = _systemTab is null
                ? _ribbon.Application.CreateRibbonPanel(_name, panelTitle)
                : _ribbon.Application.CreateRibbonPanel(_systemTab.Value, panelTitle);
            ;
            return new CustomPanel(this, ribbonPanel);
        }
    }
}
