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
        TextBlock[] blockLeft;      //Массив значений в стек
        TextBlock[] blockRight;     //Массив значений в хип
        TextBlock[] mainBlock1;     //Массив записей 1 верхнего блока
        TextBlock[] mainBlock2;     //Массив записей 2 верхнего блока
        //Button[] buttons;           //Массив кнопок
        //TextBlock[] mainBlock3;

        int whatButton = 0;         //Переменная определяет какой кнопкой добавлено дополнительное значение. 0 - общая кнопка
        string outputValue;         //Переменная для вывода


        public MainWindow()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            blockLeft = new TextBlock[] { S1, S2, S3, S4, S5, S6, S7 };
            blockRight = new TextBlock[] { H1, H1, H3, H4, H5, H6, H7 };
            mainBlock1 = new TextBlock[] { El1, El2, El3, El4, El5, El6, El7 };
            mainBlock2 = new TextBlock[] { El8, El9, El10, El11, El12, El13, El14 };
            //mainBlock3 = new TextBlock[] { El15, El16, El17, El18, El19, El20, El21 };
            //buttons = new Button[] { AddItem, Add1, Add2, Add3, Add4, Add5, Add6, Add7, Add8, Add9, Add10, Add11, Add13, Add14 };


        }


        private void AddItem_Click(object sender, RoutedEventArgs e )    //Активируем панель выбора элемента
        {
            InputPanel.IsEnabled = true;
            InputPanel.Visibility = Visibility.Visible;
            
            Button sendNameO = (Button)sender;
            string sendName = sendNameO.Name.ToString();
            sendName = sendName.Substring(3);                           //Узнаем какую кнопку нажал юзер
            if (sendName == "Item")
                whatButton = 0;                                         //Юзер нажал главную
            else
                whatButton = Convert.ToInt32(sendName);                 //Юзер нажал одну из дополнительных. Получаем индекс.
            
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
                itemTypePosition = 0;
            }
            else
            {
                MessageBox.Show("Не выбран не один тип");
            }


            AddNamePanel.Visibility = Visibility.Visible;       //Активируем панель ввода названия переменной

        }

        private void AddName_Click(object sender, RoutedEventArgs e)     //Определение куда записывать данные. В стек или хип.
        {
            itemName = NameField.Text;
            outputValue = $"{itemType}.{itemName}";
            

            if (itemTypePosition == 0)                                  
            {
                EnterBlock(blockLeft);
                
            }
            if (itemTypePosition == 1)
            {
                EnterBlock(blockRight);

                foreach (TextBlock i in blockLeft)                      //Если данные в хипе, создаем запись со ссылкой в стеке
                {
                    if ((string)i.Text == "")
                    {
                        i.Text = $"Ссылка на: {outputValue}";
                        break;
                    }
                }
            }

        }

        

        private void EnterBlock(TextBlock[] textMassive)
        {
            
            foreach (TextBlock i in textMassive)
            {
                if ((string)i.Text == "")
                {
                    
                    i.Text = outputValue;
                    
                    InputPanel.IsEnabled = false;
                    InputPanel.Visibility = Visibility.Hidden;
                    AddNamePanel.Visibility = Visibility.Hidden;
                    if (whatButton == 0)
                    {
                        foreach (TextBlock j in mainBlock1)
                        {
                            if ((string)j.Text == "")
                            {
                                j.Text = outputValue;
                                if (j.Name == "El1" && (itemType == "enum struct" || itemType == "class interface" || itemType == "delegate" || itemType == "object"))
                                {
                                    Add1.IsEnabled = true;
                                    
                                }
                                else if (j.Name == "El2" && (itemType == "enum struct" || itemType == "class interface" || itemType == "delegate" || itemType == "object"))
                                {
                                    Add2.IsEnabled = true;
                                    
                                }
                                else if (j.Name == "El3" && (itemType == "enum struct" || itemType == "class interface" || itemType == "delegate" || itemType == "object"))
                                {
                                    Add3.IsEnabled = true;
                                    
                                }
                                else if (j.Name == "El4" && (itemType == "enum struct" || itemType == "class interface" || itemType == "delegate" || itemType == "object"))
                                {
                                    Add4.IsEnabled = true;
                                    
                                }
                                else if (j.Name == "El5" && (itemType == "enum struct" || itemType == "class interface" || itemType == "delegate" || itemType == "object"))
                                {
                                    Add5.IsEnabled = true;
                                    
                                }
                                else if (j.Name == "El6" && (itemType == "enum struct" || itemType == "class interface" || itemType == "delegate" || itemType == "object"))
                                {
                                    Add6.IsEnabled = true;
                                    
                                }
                                else if (j.Name == "El7" && (itemType == "enum struct" || itemType == "class interface" || itemType == "delegate" || itemType == "object"))
                                {
                                    Add7.IsEnabled = true;
                                    
                                }
                                break;
                            }
                            


                        }
                        break;
                    }
                    else if (whatButton > 0 && whatButton <= 7)
                    {
                        foreach (TextBlock j in mainBlock2)
                        {
                            
                            if ((string)j.Text == "" && (whatButton == (Convert.ToInt32(j.Name.Substring(2))) - 7))
                            {
                                
                                if (j.Name == "El8" && (itemType == "enum struct" || itemType == "class interface" || itemType == "delegate") && whatButton == 1)
                                {
                                    Add8.IsEnabled = true;
                                    j.Text = $"{El1.Text}.{itemName}";
                                }
                                else if (j.Name == "E9" && (itemType == "enum struct" || itemType == "class interface" || itemType == "delegate") && whatButton == 2)
                                {
                                    Add9.IsEnabled = true;
                                    j.Text = $"{El2.Text}.{itemName}";
                                }
                                else if (j.Name == "El10" && (itemType == "enum struct" || itemType == "class interface" || itemType == "delegate") && whatButton == 3)
                                {
                                    Add10.IsEnabled = true;
                                    j.Text = $"{El3.Text}.{itemName}";
                                }
                                else if (j.Name == "El11" && (itemType == "enum struct" || itemType == "class interface" || itemType == "delegate") && whatButton == 4)
                                {
                                    Add11.IsEnabled = true;
                                    j.Text = $"{El4.Text}.{itemName}";
                                }
                                else if (j.Name == "El125" && (itemType == "enum struct" || itemType == "class interface" || itemType == "delegate") && whatButton == 5)
                                {
                                    Add12.IsEnabled = true;
                                    j.Text = $"{El5.Text}.{itemName}";
                                }
                                else if (j.Name == "El13" && (itemType == "enum struct" || itemType == "class interface" || itemType == "delegate") && whatButton == 6)
                                {
                                    Add13.IsEnabled = true;
                                    j.Text = $"{El6.Text}.{itemName}";
                                }
                                else if (j.Name == "El14" && (itemType == "enum struct" || itemType == "class interface" || itemType == "delegate") && whatButton == 7)
                                {
                                    Add14.IsEnabled = true;
                                    j.Text = $"{El7.Text}.{itemName}";
                                }
                                break;
                            }



                        }
                        
                        break;
                    }
                }
            }
        }
    
        
    }

}
