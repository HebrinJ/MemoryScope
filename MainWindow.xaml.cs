using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace MemoryScope
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string itemName;            //Название элемента пользователя
        string itemType;            //Тип выбранного пользователем элемента
        byte itemTypePosition;      //Расположение: 0 - stack, 1 - heap
        TextBlock[] blockLeft;            //Массив значений в стек
        TextBlock[] blockRight;              //Массив значений в хип
        TextBlock[] mainBlock1;
        TextBlock[] mainBlock2;
        TextBlock[] mainBlock3;
        
        int whatButton = 0;
        string outputValue;         //Переменная для вывода

        
        public MainWindow()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            
            blockLeft = new TextBlock[] { S1 , S2, S3, S4, S5, S6, S7 };
            blockRight = new TextBlock[] { H1, H1, H3, H4, H5, H6, H7};
            mainBlock1 = new TextBlock[] {El1, El2, El3, El4, El5, El6, El7 };
            mainBlock2 = new TextBlock[] { El8, El9, El10, El11, El12, El13, El14 };
            mainBlock3 = new TextBlock[] { El15, El16, El17, El18, El19, El20, El21 };
            

        }

        
        private void AddItem_Click(object sender, RoutedEventArgs e)    //Активируем паренль ввода
        {
            InputPanel.IsEnabled = true;
            InputPanel.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)     //Проверяем какая радиокнопка выбрана
        {
                        
            if (NameByte.IsChecked == true)
            {
                itemType = "(s)byte (u)short";
                itemTypePosition = 0;
            }
            else if (NameObject.IsChecked == true)
                        {
                itemType = "object";
                itemTypePosition = 1;
            }
            else if (NameInt.IsChecked == true)
            {
                itemType = "(u)int (u)long";
                itemTypePosition = 0;
            }
            else if (NameString.IsChecked == true)
            {
                itemType = "string";
                itemTypePosition = 1;
            }
            else if (NameFloat.IsChecked == true)
            {
                itemType = "float double";
                itemTypePosition = 0;
            }
            else if (NameClass.IsChecked == true)
            {
                itemType = "class interface";
                itemTypePosition = 1;
            }
            else if (NameDec.IsChecked == true)
            {
                itemType = "decimal bool char";
                itemTypePosition = 0;
            }
            else if (NameDelegate.IsChecked == true)
            {
                itemType = "delegate";
                itemTypePosition = 1;
            }
            else if (NameEnum.IsChecked == true)
            {
                itemType = "enum struct";
                itemTypePosition = 1;
            }
            else
            {
                MessageBox.Show("Не выбран не один тип");
            }
            
            
            AddNamePanel.Visibility = Visibility.Visible;

        }

        private void AddName_Click(object sender, RoutedEventArgs e)
        {
            itemName = NameField.Text;
            outputValue = $"{itemType}.{itemName}";
            
            if (itemTypePosition == 0)
            {
                
                foreach (TextBlock i in blockLeft)
                {
                    if ((string)i.Text == "")
                    {
                        i.Text = outputValue;
                        InputPanel.IsEnabled = false;
                        InputPanel.Visibility = Visibility.Hidden;
                        AddNamePanel.Visibility = Visibility.Hidden;
                        foreach (TextBlock j in mainBlock1)
                        {
                            if ((string)j.Text == "")
                            {
                                j.Text = outputValue;
                                if (j.Name == "El1")
                                    Add1.IsEnabled = true;
                                else if (j.Name == "El2")
                                    Add2.IsEnabled = true;
                                else if (j.Name == "El3")
                                    Add3.IsEnabled = true;
                                else if (j.Name == "El4")
                                    Add4.IsEnabled = true;
                                else if (j.Name == "El5")
                                    Add5.IsEnabled = true;
                                else if (j.Name == "El6")
                                    Add6.IsEnabled = true;
                                else if (j.Name == "El7")
                                    Add7.IsEnabled = true;
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            if (itemTypePosition == 1)
                foreach (TextBlock i in blockRight)
                {
                    if ((string)i.Text == "")
                    {
                        i.Text = outputValue;
                        InputPanel.IsEnabled = false;
                        InputPanel.Visibility = Visibility.Hidden;
                        AddNamePanel.Visibility = Visibility.Hidden;
                        foreach (TextBlock j in mainBlock1)
                        {
                            if ((string)j.Text == "")
                            {
                                j.Text = outputValue;
                                if (j.Name == "El1")
                                    Add1.IsEnabled = true;
                                else if (j.Name == "El2")
                                    Add2.IsEnabled = true;
                                else if (j.Name == "El3")
                                    Add3.IsEnabled = true;
                                else if (j.Name == "El4")
                                    Add4.IsEnabled = true;
                                else if (j.Name == "El5")
                                    Add5.IsEnabled = true;
                                else if (j.Name == "El6")
                                    Add6.IsEnabled = true;
                                else if (j.Name == "El7")
                                    Add7.IsEnabled = true;
                                break;
                                
                            }
                        }
                        break;
                    }
                }


        }

        private void Add1_Click(object sender, RoutedEventArgs e)
        {
            Button sendNameO = (Button)sender;
            string sendName = sendNameO.Name.ToString();
            sendName = sendName.Substring(3);
            whatButton = Convert.ToInt32(sendName);
            
            
            
            
        }
    }
}
