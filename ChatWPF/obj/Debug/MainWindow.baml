   M S B A M L   `   `   `  ����B  >ChatWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ChatWPFChatWPF  5   	       ChatWPF.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:ChatWPF     5   	     � Title$  
MainWindow��5   	   $	��489��6   $	��650��6)   .��5	      � ���	#FF5B5151$	��NaN��6"    � ColumnDefinitions 5
   
   �� 5      $	��50* �6   �� 5      $	��50* �6    � RowDefinitions 5   
   �� 5      $	��63* �6   �� 5      $	��45* �6   �� 5      $	��84* �6   �� 5      $
��281* �6   .+�5   
   �� -   5        $��ServerCreate��5      $��0��6%   $!��Стать сервером��62   $��80,10,80,10q�6K   
���  �����	#FFFDB4B4$��2��6�   $��20(�6�   �� 5   
   -   5        $��ServerConect��5      $0��*Подключиться к серверу��6%   $��80,10,80,10q�6F   $��Center=�6[   $	��Top=�6x   $	��300��6�   $��50��6�   
���  �����	#FFFDB4B4
���   �$��3��6�   $��2��6  $��20(�6   �� 5   
   -   5        $��UserNameTextBox��5   /   $��2��6   $��0��6   $��10q�6F    �� TextWrapping$
 Wrap=�6R   ���	#FFFF1700$��20(�6   �� 5   
   $��1��6   $��0��6!   $
��Wrap=�61   $%��Имя пользавателя��6E   
������� J� OpacityMask
 �   � �� TextAlignment$ Center=�6�   $��20(�6�   �� 5   
   $��1��6   $��1��6!   $
��Wrap=�61   $��IP��6E   
�������
 �   �$��20(�6v   $ Center=�6�   �� 5   
   $��2��6   $��1��6"    �� Orientation$ 
Horizontal=�62   $��Center=�6K   .+�5      �� -   5        $��ServerIpTextBox1��5   !   $	��192��6   ���	#FFFF1700$��70��6Q   $��5q�6\   $��20(�6g   $��Stretch=�6u   $��Stretch=�6�   �� 5      -   5         ��Name$ ServerIpTextBox2��5   !   $	��168��6   ���	#FFFF1700$��70��6S   $��5q�6^   $��20(�6i   $��Stretch=�6w   $��Stretch=�6�   �� 5      -   5        $ ServerIpTextBox3��5       $��50��6   ���	#FFFF1700$��70��6R   $��5q�6]   $��20(�6h   $��Stretch=�6v   $��Stretch=�6�   �� 5       -   5        $ ServerIpTextBox4��5    !   $	��106��6   ���	#FFFF1700$��70��6S   $��5q�6^   $��20(�6i   5!      5#      5$      