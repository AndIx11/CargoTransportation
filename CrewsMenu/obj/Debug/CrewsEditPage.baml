   M S B A M L   `   `   `  ����D  @CrewsMenu, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CrewsMenu	CrewsMenu  5          CrewsMenu.CrewsEditPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:CrewsMenu     5        N� Title$  BanksEditPage��5	      .��5      � 
�������.+�5   
   �� -   5         ��Name$ LogOutButton��5      $��
Выйти��6'   $��Right=�67   $��	0,32,50,0q�6S   $	��Top=�6f   $	��114��6~   $��36��6�   ��6�   �� *LogOutCommand+&   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 	MainTable��5      $��50,85,50,50q�6&   ��. $
��Auto=�6`   $	��385��6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ #��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 45 6G   1  +System.Windows.Controls.DataGridBoundColumn   Binding 60   �� *Id+6G     5      $ Название��6%   $ * 6P    67   �� *Name+6P   5      �� 5   
   -   5        $ 	AddButton��5      $��Добавить��6$   $
��Left=�67   $��	51,32,0,0q�6R   $	��128��6e   $��37��6q   $	��Top=�6}   �� 5   
   -   5        $ 
EditButton��5      $ ��Редактировать��6%   $
��Left=�6=   $��
198,32,0,0q�6X   $	��162��6l   $��37��6x   $	��Top=�6�   �� 5   
   -   5        $ DeleteButton��5      $��Удалить��6'   $
��Left=�69   $��
373,32,0,0q�6T   $	��Top=�6h   $	��128��6�   $��37��6�   �� 5   
   -   5        $ 
ReadButton��5      $��Инфомация��6%   $
��Left=�69   $��
133,10,0,0q�6T   $	��Top=�6h   $	��128��6�   $��37��6�   $y�Hidden=�6�   5      5      