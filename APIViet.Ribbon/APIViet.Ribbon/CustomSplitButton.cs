//Learn the solution of Victor Chekalin

#region Namespaces
using System;
using System.Collections.Generic;
using Autodesk.Revit.UI;
using System.ComponentModel;
#endregion

namespace APIViet.Ribbon
{
    public class CustomSplitButton : CustomPushButton
    {
        private readonly IList<CustomPushButton> _buttons = new List<CustomPushButton>();
        public CustomSplitButton(string name, string text) : base(name, text, null)
        {
        }
        internal override ButtonData GetButtonData()
        {
            SplitButtonData splitButtonData =
                new SplitButtonData(_name,
                    _text);
            if (_largeImage != null)
            {
                splitButtonData.LargeImage = _largeImage;
            }

            if (_smallImage != null)
            {
                splitButtonData.Image = _smallImage;
            }
            if (_toolTipsImage != null)
            {
                splitButtonData.ToolTipImage = _toolTipsImage;
            }
            if (!string.IsNullOrWhiteSpace(_toolTips))
            {
                splitButtonData.ToolTip = _toolTips;
            }
            if (!string.IsNullOrWhiteSpace(_description))
            {
                splitButtonData.LongDescription = _description;
            }
            if (_contextualHelp != null)
            {
                splitButtonData.SetContextualHelp(_contextualHelp);
            }
            return splitButtonData;
        }
        public CustomSplitButton CreateButton(string name, string text, Type externalCommandType, Action<CustomPushButton> action)
        {
            var button = new CustomPushButton(name, text, externalCommandType);
            action?.Invoke(button);
            Buttons.Add(button);
            return this;
        }
        public CustomSplitButton CreateButton(string name, string text, Type externalCommadType)
        {
            return CreateButton(name, text, externalCommadType, null);
        }
        public CustomSplitButton CreateButton<TExternalCommanClass>(string name, string text)
                                where TExternalCommanClass: class, IExternalCommand
        {
            var commandClassType = typeof(TExternalCommanClass);
            return CreateButton(name, text, commandClassType, null);
        }

        public CustomSplitButton CreatButton<TExternalCommandClass>(string name, string text, Action<CustomPushButton> action)
                                    where TExternalCommandClass: class, IExternalCommand
        {
            var commandClassType = typeof(TExternalCommandClass);
            return CreateButton(name, text, commandClassType, action);
        }

        public int ItemsCount => Buttons.Count;

        public IList<CustomPushButton> Buttons => _buttons;

        internal void BuildButtons(Autodesk.Revit.UI.SplitButton splitbutton)
        {
            foreach (var button in Buttons)
            {
                splitbutton.AddPushButton(button.GetButtonData() as PushButtonData);
            }
        }
    }
    
}
