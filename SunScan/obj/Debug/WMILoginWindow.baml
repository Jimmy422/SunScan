   M S B A M L   `   `   `  ����B  >SunScan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:SunScanSunScan  5   	       SunScan.WMILoginWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:SunScan     5   	   -   5          � Title$  Log In��5   	   $	��325��6   $	��500��6%    � 
ResizeMode$ NoResize=�61    � Icon$ assets/icon.ico��6G   .��5	      � ���	#FF1D1D1D � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ header_page��5      
�������$��0��6<   $��36(�6I   $��Segoe UI Light)�6W   $��12 0 0 0q�6s      Header_Page#���   6�   Log in to WMI5      �� 5   
    �� Orientation$ Vertical=�6   $��1��6,   .+�5      �� $ 
Horizontal=�6   $��
16 24 16 0q�62   .+�5      �� $��Center=�6   
�������$��0 0 16 0q�6J   $��18(�6\   	Username:6u   �� 5      -   5         ��Name$ textbox_username��5   ^     Textbox_search#���  6   $��Center=�6C   $	��362��6x   $��30��6�   5      �� 5      $ 
Horizontal=�6   $��
16 12 16 0q�62   .+�5      �� $��Center=�6   
�������$��0 0 22 0q�6J   $��18(�6\   	Password:6u   G� 5      -   5         G�Name$ textbox_password��5   f     Passwordbox_search#���  6   $��Center=�6K   $	��362��6�   $��30��6�   5      �� 5      $ 
Horizontal=�6   $��16 12 16 16q�62   .+�5      �� $��Center=�6   
�������$��0 0 34 0q�6J   $��18(�6\   	Domain:6s   �� 5      -   5        $ textbox_domain��5   ^   #���  6   $��Center=�6C   $	��362��6v   $��30��6�   5      �� 5      $ 
Horizontal=�6   $��
16 0 16 16q�62   .+�5      �� -   5         ��Name$ button_connect��5   T     Button_Generic#���  6   $��0 0 16 0q�6B   .��5       �� $ 
Horizontal=�6!   .+�5!      �� $��18(�6$   $��Center=�62   $��Center=�6M   $	����6j   $��Segoe MDL2 Assets)�6z   $��6,0,6,0q�6�   6�   �� 5"      $��14(�6$   $��Center=�62   $��Center=�6M   $��6,0,12,0q�6j   	Connect6�   5#      5$      �� 5%      -   5        $ button_cancel��5%   B   #���  6   .��5&      �� $ 
Horizontal=�6!   .+�5'      �� $��18(�6$   $��Center=�62   $��Center=�6M   $	����6j   $��Segoe MDL2 Assets)�6z   $��6,0,6,0q�6�   6�   �� 5(      $��14(�6$   $��Center=�62   $��Center=�6M   $��6,0,12,0q�6j   Cancel6�   5)      5*      5+      �� 5,      -   5        $ textbox_status��5,   ?   $��16 0 0 0q�6   $y�Visible=�6*   
�������$��12(�6j   $��Center=�6x   	Status:6�   5-      5.      5/      