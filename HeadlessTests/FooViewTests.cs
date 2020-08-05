using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using JetBrains.Annotations;
using TestApp;
using Xunit;

namespace HeadlessTests
{
    public class FooViewTests
    {
        public class TestModel : INotifyPropertyChanged
        {
            private string _myText;
            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public string MyText
            {
                get => _myText;
                set
                {
                    if (value == _myText) return;
                    _myText = value;
                    OnPropertyChanged();
                }
            }
        }
        
        [Fact]
        public void TextBox_Is_Bound_To_MyText()
        {
            var model = new TestModel
            {
                MyText = "SomeText"
            };
            var view = new FooView()
            {
                DataContext = model
            };
            var window = new Window()
            {
                Content = view
            };
            window.Show();
            var tb = view.FindControl<TextBox>("MainText");
            Assert.Equal("SomeText", tb.Text);

        }
    }
}