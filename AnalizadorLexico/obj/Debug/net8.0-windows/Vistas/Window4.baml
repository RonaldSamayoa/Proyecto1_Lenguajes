   M S B A M L   `   `   `  ����K  GAnalizadorLexico, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:AnalizadorLexico.VistasAnalizadorLexico.Vistas  5   	   %    AnalizadorLexico.Vistas.Window4   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:AnalizadorLexico.Vistas     5   	     � Title$  Buscar Patrón��5   	   $	��168��6   $	��500��6,   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   $��0,0,0,10q�6;   .+�5      �� $��Cadena a buscar:��6   $��Center=�6/   �� 5      -   5         ��Name$ 	txtPatron��5      $	��200��6)   $
��10,0q�65   �� 5      -   5        $��Buscar��5      $��75��6;   5      �� 5   
   -   5         ��Name$ txtResultado��5      $��1��6*   $
��Bold"�67   
����  �$��0,0,0,10q�6_   �� 5   
   -   5        $��Cerrar��5      $��2��6"   $��75��6/   $��Right=�6:   6V   5      5      