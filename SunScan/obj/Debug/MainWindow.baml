   M S B A M L   `   `   `  ����B  >SunScan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:SunScanSunScan  5   	       SunScan.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:SunScan     5   	     � Title$  SunScan��5   	   $	��600��6   $
��1100��6&    � 
ResizeMode$ CanResizeWithGrip=�63    � Icon$ assets/icon.ico��6R   .��5	      � ���	#FF1D1D1D � RowDefinitions 5
   
   �� 5      $��* �6   6)    � ColumnDefinitions 5   
   �� 5      $	��200 �6   6-   �� 5      $��* �6   6+   .+�5   
   �� .+�5      �� -   5         ��Name$ button_home��5      $��80��6*      
SideButton#���   66   .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��	24 0 20 0q�6    $��Center=�63   $��Segoe MDL2 Assets)�6N   ���	#FFEA85356�   �� 5      $��Center=�6    
�������Home6T   5      5      �� 5%      -   5        $ button_favorites��5%      $��80��6N   #���   6Z   .��5&      �� $ 
Horizontal=�6   .+�5'      �� $��	24 0 20 0q�6    $��Center=�63   $��Segoe MDL2 Assets)�6N   ���	#FFEA85356�   �� 5(      $��Center=�6    
�������	Favorites6Y   5)      5*      �� 5+      -   5        $ button_settings��5+      $��80��6L   #���   6X   .��5,      �� $ 
Horizontal=�6   .+�5-      �� $��	24 0 20 0q�6    $��Center=�63   $��Segoe MDL2 Assets)�6N   ���	#FFEA85356�   �� 5.      $��Center=�6    
�������
Settings6X   5/      50      51      � 52   
   $��1��6   ���	#FF313131.+�53       � -   5          �Name$ frame_pages��53        � NavigationUIVisibility$ Hidden=�6)   $��Frame��6I   	  � Source$	 Pages/HomePage.xaml?�6Y   $
��12,6q�6v   54      55      56      