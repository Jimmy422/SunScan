   M S B A M L   `   `   `  ����B  >SunScan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:SunScan.PagesSunScan.Pages  5           SunScan.Pages.SettingsPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:SunScan.Pages     5        N� Title$  DeviceDetailsPage��5	      .��5      �  � RowDefinitions 5   
   �� 5      $��75 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��60 �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ button_back��5      $��2��6&   $	����6:   $
��Left=�6M   $	��Top=�6h   $��60��6�   $��60��6�      TransparentBackgroundButton#���   6�   $��Segoe MDL2 Assets)�6�   �� 5   
   $��0��6   $��1��6!   
�������$��36(�6D   $��Segoe UI Light)�6R     Header_Page#���  6n   $��Settings��6�   � 5   
   $��2��6   $��1��6#     ScanInfoBox#���  60   .+�5      �� .+�5      ��  �� Orientation$ 
Horizontal=�6   $��	0 0 16 16q�66   .+�5      �� $��Center=�6    
�������$��0 0 16 0q�6N   $��18(�6`   NMAP Command Being Ran:6�   �� 5      -   5         ��Name$ textbox_nmapcommand��5       $��Center=�6=   
�������$��0 0 16 0q�6k   $��18(�6}   	Command6�   5      �� 5      $ 
Horizontal=�6   $��	0 0 16 16q�66   .+�5      �� $��Center=�6    
�������$��0 0 16 0q�6N   $��18(�6`   IP Address Range (+/-):6�   �� 5      -   5         ��Name$ 
ipRangeBox��5   b     Textbox_search#���  6   $��Center=�6G   $��30��6v   $��25��6�   5      �� 5       $ 
Horizontal=�6   $��	0 0 16 16q�66   .+�5!      �� $��Center=�6    $��1��6;   $��0 0 16 0q�6H   
�������$��18(�6m   Overwrite Scan IP Address:6�   �� 5"      -   5         ��Name$ checkBox_overwrite��5"      $��Center=�6;    d� 	IsChecked$ FalseY�6V   6�   �� 5#      $ 
Horizontal=�6!   $��16 0 0 0q�6:   .+�5$      �� $��Center=�6$   
�������$��0 0 16 0q�6R   $��18(�6d   IP Address To Overwrite:6�   �� 5%      -   5        $ overwriteIPBox��5%   f   #���  6"   $��Center=�6K   $	��100��6~   $��30��6�   5&      5'      �� 5(      $ 
Horizontal=�6   $��	0 0 16 16q�66   .+�5)      �� $��Center=�6    $��1��6;   $��0 0 16 0q�6H   
�������$��18(�6m   Attempt To Find Device Names:6�   �� 5*      -   5        $ checkBox_findNames��5*      $��Center=�6;   $ FalseY�6V   6�   �� 5+      $ 
Horizontal=�6!   $��16 0 0 0q�6:   .+�5,      �� $��Center=�6$   
�������$��0 0 16 0q�6R   $��18(�6d   &$Use System DNS To Find Device Names:6�   �� 5-      -   5        $ checkBox_useSystemDNS��5-   #   $��Center=�6B   $ FalseY�6]   6�   5.      5/      �� 50      $ 
Horizontal=�6   $��	0 0 16 16q�66   .+�51      �� $��Center=�6    
�������$��0 0 36 0q�6N   $��18(�6`   Default WMI Credentials:6�   �� 52      $��Center=�6    
�������$��0 0 8 0q�6N   $��18(�6_   	Username:6x   �� 53      -   5        $ wmiUsername��53   t   #���  6   $��Center=�6G   $��0 0 16 0q�6b   $	��100��6�   $��30��6�   �� 54      $��Center=�6    
�������$��0 0 8 0q�6N   $��18(�6_   	Password:6x   G� 55      -	   5        	 G�Name$	 wmiPassword��55   |     Passwordbox_search#���  6"   $��Center=�6O   $��0 0 16 0q�6j   $	��100��6�   $��30��6�   �� 56      $��Center=�6    
�������$��0 0 8 0q�6N   $��18(�6_   	Domain:6v   �� 57      -
   5        $ 	wmiDomain��57   b   #���  6   $��Center=�6G   $	��100��6u   $��30��6�   58      �� 59      $ 
Horizontal=�6   $��	0 0 16 16q�66   .+�5:      �� -   5        $ button_saveSettings��5:   X     Button_Generic#���  6   $��0 0 16 0q�6F   .��5;      �� $ 
Horizontal=�6%   .+�5<      �� $��18(�6(   $��Center=�66   $��Center=�6Q   $	����6n   $��Segoe MDL2 Assets)�6~   $��6,0,6,0q�6�   6�   �� 5=      $��14(�6(   $��Center=�66   $��Center=�6Q   $��6,0,12,0q�6n   Save Settings6�   5>      5?      �� 5@      -   5        $ button_clearFavorites��5@   F   #���  6   .��5A      �� $ 
Horizontal=�6%   .+�5B      �� $��18(�6(   $��Center=�66   $��Center=�6Q   $	����6n   $��Segoe MDL2 Assets)�6~   $��6,0,6,0q�6�   6�   �� 5C      $��14(�6(   $��Center=�66   $��Center=�6Q   $��6,0,12,0q�6n   Clear Favorites6�   5D      5E      �� 5F      -   5        $ textbox_settings_saved��5F   7   $y�	Collapsed=�6    
�������$��18(�6j   $��Center=�6x   Settings Saved6�   5G      �� 5H      .+�5I      �� $��0��6    $��1��6-   
�������$��36(�6P   $��Segoe UI Light)�6^   #���  6{   $��About Sunscan 1.0.0��6�   �� 5J      $��Center=�6    $��1��6;   $��0 0 0 24q�6H   
�������$��18(�6m   XVDesigned for SunView Inc. for University of South Florida Senior Project - Spring 20166�   �� 5K      $��Center=�6    $��1��6;   $��	0 0 16 12q�6H   
�������$��18(�6n   ><UI, WMI Querying/Login, and NMAP Scan Parsing by James Keith6�   �� 5L      $��Center=�6    $��1��6;   $��	0 0 16 12q�6H   
�������$��18(�6n   '%IP Retreval and Setup by Roger Milton6�   �� 5M      $��Center=�6    $��1��6;   $��	0 0 16 12q�6H   
�������$��18(�6n   HFNetwork Technique, Management Research and WMI Querying by Seth Kenney6�   5N      5O      5Q      5R      5S      