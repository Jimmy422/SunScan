   M S B A M L   `   `   `  ����B  >SunScan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:SunScan.PagesSunScan.Pages  5      !    SunScan.Pages.FavoritesPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:SunScan.Pages     5        N� Title$  ResultsPage��5	      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   
�������$��36(�64   $��Segoe UI Light)�6B      Header_Page#���   6_   Scan Results5      � 5   
   -   5         �Name$ grid_results��5      $��1��6%    5      �� 5      $
��Auto �6    �� 5      $��* �6     � ColumnDefinitions 5      �� 5      $��* �6#   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��0,0,0,12q�62   .+�5      �� -   5         ��Name$ button_sort_az��5        Button_Generic#���  6N   .��5      �� $ 
Horizontal=�6!   .+�5      �� $��18(�6$   $��Center=�62   $��Center=�6M   $	����6j   $��Segoe MDL2 Assets)�6z   $��6,0,6,0q�6�   6�   �� 5      $��14(�6$   $��Center=�62   $��Center=�6M   $��6,0,12,0q�6j   Sort List A-Z6�   5       5!      �� 5"      -   5        $ button_sort_za��5"      #���  6N   .��5#      �� $ 
Horizontal=�6!   .+�5$      �� $��18(�6$   $��Center=�62   $��Center=�6M   $	����6j   $��Segoe MDL2 Assets)�6z   $��6,0,6,0q�6�   6�   �� 5%      $��14(�6$   $��Center=�62   $��Center=�6M   $��6,0,12,0q�6j   Sort List Z-A6�   5&      5'      �� 5(      -   5        $ button_ip_asc��5(      #���  6L   .��5)      �� $ 
Horizontal=�6!   .+�5*      �� $��18(�6$   $��Center=�62   $��Center=�6M   $	����6j   $��Segoe MDL2 Assets)�6z   $��6,0,6,0q�6�   6�   �� 5+      $��14(�6$   $��Center=�62   $��Center=�6M   $��6,0,12,0q�6j   
Reset List6�   5,      5-      �� 5.      $��14(�6   $��Center=�6*   
�������$��24,0,6,0q�6X   Search List:6x   �� 5/      -   5         ��Name$ textBox��5/       �� TextWrapping$
 Wrap=�6+   $	��240��6?   $��27��6K     Textbox_search#���  6W   6�   50      �� 51      -   5         ��Name$ listBox_devices��51   _   $��1��6     Listitem_Scan_Result#���  6#   $
��Left=�6x   $	��Top=�6�   	 � IsSharedSizeScope	 .   Device_ListBox#���  6y  ��6�   �� 
 �� ElementName$
 grid_results�� �� Path$ ActualWidth���6  �� $
 grid_results��$ ActualHeight���53      �� 54      .R�55      � .+�56      �� $ 
Horizontal=�6)   .+�57   "   �� $��42(�6,   $��Center=�6:   $��Center=�6U   $	����6r   
�������$��Segoe MDL2 Assets)�6�   $��6,0,12,0q�6�   6�   �� 58   "   $ Vertical=�6-   .+�59   &   �� $ 
Horizontal=�61   .+�5:   *   �� $��18(�64   ��6B   �� *
deviceName+�� 5;   *   $��18(�64   $	�� | ��6B   �� 5<   *   $��18(�64   ��6B   �� *devicePCName+5=   '   �� 5>   &   $ 
Horizontal=�61   .+�5?   *   �� $��14(�64   $��IP Address: ��6B   �� 5@   *   $��14(�64   ��6B   �� *
deviceIP+5A   '   �� 5B   &   $ 
Horizontal=�61   .+�5C   *   �� $��14(�64   $��MAC Address: ��6B   �� 5D   *   $��14(�64   ��6B   �� *	deviceMAC+5E   '   �� 5F   &   $ 
Horizontal=�61   .+�5G   *   �� $��14(�64   
�������$��WMI Management ��6U   �� 5H   *   $��14(�64   ��6B   �� *wmiManageableText+5I   '   5J   #   5K      5L      5M      5O      5P      5Q      5S      