   M S B A M L   `   `   `  ����B  >ChatWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ChatWPFChatWPF  5   	       ChatWPF.Server   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:ChatWPF     5   	   -   5          � Title$  
MainWindow��5   	   $	��766��6   $	��576��6)   .��5	      � $	��NaN��6   ���	#FFFF7878 � ColumnDefinitions 5
   
   �� 5      $	��46* �6   �� 5      $	��25* �6    � RowDefinitions 5   
   �� 5      $	��52* �6   �� 5      $
��241* �6   �� 5      $	��50* �6   �� 5      $	��50* �6   .+�5   
   �� -   5         ��Name$ ChatIpTB��5      $��0��6&   $��Center=�66   $��Center=�6S   $
��Wrap=�6n   $��IP чата: ��6�   $��20(�6�   $��	10,0,10,0q�6�   $��2��6�   �� 5   
   -   5         ��Name$ MessagesLbx��5   "   $��0��6   $��	0,104,0,0q�67   $��2��6J   ���	#FF5B51516[   �� 5   
   -   5        $ 
ClientsLbx��5      $��2��6&   $��373,104,0,0q�6:   $��2��6O   ���	#FFFDB4B4
���  ��6w   �� 5   
   -   5        $��	MessageTb��5   @   $��2��6   $��0��6   $��2��6/   $��
10,10,10,4q�6Q   ���	#FF5B51516~   �� 5   
   -   5        $��Отправить��5      $��1��6%   $��2��65   $��10,10,10,10q�6B   $��20(�6W   
���  �����	#FFFDB4B46�   �� 5   
   -   5        $��Выгрузка��5      $��1��6$   $��3��64   $��10,10,10,10q�6A   $��20(�6V   
���  �����	#FFFDB4B46�   5      5      