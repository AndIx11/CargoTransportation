   M S B A M L   `   `   `  ����L  HAccessControlMenu, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:AccessControlMenuAccessControlMenu  5      )    #AccessControlMenu.AccessControlPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:AccessControlMenu     5        N� Title  5	      �� *Tittle+.��5      � 
�������.+�5   
   �� -   5         ��Name$ LogOutButton��5      $��
Выйти��6'      StyleButtonExit#��[  67   $��Right=�6`   $��	0,32,50,0q�6|   $	��Top=�6�   $	��114��6�   $��36��6�   ��6�   �� *LogOutCommand+&   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 	MainTable��5        CustomDataGridStyle#��[ 6&   $��50,85,50,50q�6S   ��. $
��Auto=�6�   $	��385��6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ #��6      Width5  /System.Windows.Controls.DataGridLengthConverter$ 25 6;   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6$   �� *Id+6;     5      $ Пользователь��6   $ * 6N    6/   �� *
User.Login+6N     5      $ Меню��6   $ * 6I    6'   �� *MenuItem.Name+6I   4  .System.Windows.Controls.DataGridCheckBoxColumn  5      $ Чтение��6   $ * 6F    6-   �� *Read+6F     5      $ Запись��6   $ * 6F    6-   �� *Add+6F     5      $" Редактирование��6   $ * 6N    65   �� *Edit+6N     5      $ Удаление��6   $ * 6J    6/   �� *Delete+6J   5      �� 5   
   -   5        $ 	AddButton��5      $��Новый доступ��6$   $
��Left=�6;   $��	51,32,0,0q�6V   $	��Top=�6i   $	��146��6�   $��37��6�     StyleButtonOk#��[ 6�   �� 5   
   -   5        $ 
EditButton��5      $ ��Редактировать��6%   $
��Left=�6=   $��
206,32,0,0q�6X   $	��Top=�6l   $	��162��6�   $��37��6�     StyleButtonEdit#��[ 6�   �� 5   
   -   5        $ DeleteButton��5      $��Удалить��6'   $
��Left=�69   $��
379,32,0,0q�6T   $	��Top=�6h   $	��128��6�   $��37��6�   #��[ 6�   �� 5   
   -   5        $ 
ReadButton��5      $��Инфомация��6%   $
��Left=�69   $��
133,10,0,0q�6T   $	��Top=�6h   $	��128��6�   $��37��6�   $y�Hidden=�6�   5      5      