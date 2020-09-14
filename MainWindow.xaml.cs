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
        TextBlock[] mainBlock3;

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
            mainBlock3 = new TextBlock[] { El15, El16, El17, El18, El19, El20, El21 };
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

        

        private void EnterBlock(TextBlock[] textMassive)    //Определение блока для отображение данных и активации кнопок
        {
            
            foreach (TextBlock i in textMassive)
            {
                if ((string)i.Text == "")
                {
                    
                    i.Text = outputValue;
                    
                    InputPanel.IsEnabled = false;
                    InputPanel.Visibility = Visibility.Hidden;
                    AddNamePanel.Visibility = Visibility.Hidden;
                    if (whatButton == 0)                               //Если нажата главная кнопка
                    {
                        foreach (TextBlock j in mainBlock1)
                        {
                            if ((string)j.Text == "")               //Определяется текстовое поле для записи
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
                    else if (whatButton > 0 && whatButton <= 7)     //Если нажата кнопка первого блока
                    {
                        foreach (TextBlock j in mainBlock2)
                        {
                            
                            if ((string)j.Text == "" && (whatButton == (Convert.ToInt32(j.Name.Substring(2))) - 7))     //Определяется текстовое поле для записи
                            {
                                
                                if (j.Name == "El8" && whatButton == 1)
                                {
                                    j.Text = $"{El1.Text}.{itemName}";
                                    if (itemType == "enum struct" || itemType == "class interface" || itemType == "delegate")
                                    Add8.IsEnabled = true;
                                }
                                else if (j.Name == "El9" && whatButton == 2)
                                {
                                    j.Text = $"{El2.Text}.{itemName}";
                                    if (itemType == "enum struct" || itemType == "class interface" || itemType == "delegate")
                                    Add9.IsEnabled = true;    
                                }
                                else if (j.Name == "El10" && whatButton == 3)
                                {
                                    j.Text = $"{El3.Text}.{itemName}";
                                    if (itemType == "enum struct" || itemType == "class interface" || itemType == "delegate")
                                    Add10.IsEnabled = true;    
                                }
                                else if (j.Name == "El11" && whatButton == 4)
                                {
                                    j.Text = $"{El4.Text}.{itemName}";
                                    if (itemType == "enum struct" || itemType == "class interface" || itemType == "delegate")
                                    Add11.IsEnabled = true;    
                                }
                                else if (j.Name == "El12" && whatButton == 5)
                                {
                                    j.Text = $"{El5.Text}.{itemName}";
                                    if (itemType == "enum struct" || itemType == "class interface" || itemType == "delegate")
                                    Add12.IsEnabled = true;    
                                }
                                else if (j.Name == "El13" && whatButton == 6)
                                {
                                    j.Text = $"{El6.Text}.{itemName}";
                                    if (itemType == "enum struct" || itemType == "class interface" || itemType == "delegate")
                                    Add13.IsEnabled = true;    
                                }
                                else if (j.Name == "El14" && whatButton == 7)
                                {
                                    j.Text = $"{El7.Text}.{itemName}";
                                    if (itemType == "enum struct" || itemType == "class interface" || itemType == "delegate")
                                    Add14.IsEnabled = true;    
                                }
                                break;
                            }



                        }
                        
                        break;
                    }
                    else if (whatButton > 7)        //Если нажата кнопка второго блока
                    {
                        foreach (TextBlock j in mainBlock3)
                        {

                            if ((string)j.Text == "" && (whatButton == (Convert.ToInt32(j.Name.Substring(2))) - 7))     //Определяется текстовое поле для записи
                            {

                                if (j.Name == "El15" && whatButton == 8)
                                {
                                    
                                    j.Text = $"{El1.Text}.{El8.Text}.{itemName}";
                                }
                                else if (j.Name == "El16" && whatButton == 9)
                                {
                                    
                                    j.Text = $"{El2.Text}.{El9.Text}.{itemName}";
                                }
                                else if (j.Name == "El17" && whatButton == 10)
                                {
                                    
                                    j.Text = $"{El3.Text}.{El10.Text}.{itemName}";
                                }
                                else if (j.Name == "El18" && whatButton == 11)
                                {
                                    
                                    j.Text = $"{El4.Text}.{El11.Text}.{itemName}";
                                }
                                else if (j.Name == "El19" && whatButton == 12)
                                {
                                    
                                    j.Text = $"{El5.Text}.{El12.Text}.{itemName}";
                                }
                                else if (j.Name == "El20" && whatButton == 13)
                                {
                                    
                                    j.Text = $"{El6.Text}.{El13.Text}.{itemName}";
                                }
                                else if (j.Name == "El21" && whatButton == 14)
                                {
                                    
                                    j.Text = $"{El7.Text}.{El14.Text}.{itemName}";
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

