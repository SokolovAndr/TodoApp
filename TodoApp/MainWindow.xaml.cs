using System;
using System.ComponentModel;
using System.Windows;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp
{

    public partial class MainWindow : Window
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\todoDataList.json";
        private BindingList<TodoModel> _todoDataList; //нижнее подчеркивание для private поля
        private FileIOService _fileIOService;  

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIOService = new FileIOService(PATH);

            try 
            {
                _todoDataList = _fileIOService.LoadData();
            }
            catch (Exception ex) //если возникает ошибка
            {
                MessageBox.Show(ex.Message);
                Close();
            }


            DataGridTest.ItemsSource = _todoDataList;
            _todoDataList.ListChanged += _todoDataList_ListChanged;
        }

        private void _todoDataList_ListChanged(object? sender, ListChangedEventArgs e)
        {
            if(e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    _fileIOService.SaveData(sender);
                }
                catch (Exception ex) //если возникает ошибка
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }

            }
        }
    }
}