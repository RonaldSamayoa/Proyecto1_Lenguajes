   M S B A M L   `   `   `  ����K  GAnalizadorLexico, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:AnalizadorLexico.VistasAnalizadorLexico.Vistas  5   	   %    AnalizadorLexico.Vistas.Window1   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:AnalizadorLexico.Vistas     5   	     � Title$  Analizador Léxico��5   	   $	��450��6#   $	��800��60   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��29* �6   �� 5      $
��371* �6   .+�5   
   o� $	��Top=�6   $��54��6'   .@�5      m� $��Archivo��6   .@�5      m� -   5        $��Nuevo��5      6*   m� 5      -   5        $��Abrir��5      6*   m� 5      -   5        $��Guardar��5      6,   m� 5      -   5        $��Guardar Como...��5      64   �� 5      m� 5      -   5        $��Salir��5      6*   5      m� 5      $��Edición��6   .@�5      m� -   5        $��Copiar��5      6+   m� 5      -   5        $��Pegar��5      6*   m� 5      -   5        $��Deshacer��5      6-   m� 5      -	   5        $��Rehacer��5      6,   5      m� 5      $��Ayuda��6   .@�5      m� -
   5        $��Acerca de...��5      61   5      5       �� 5"   
   -   5         ��Name$ btnAnalizar��5"      $��Analizar��6&   $
��Left=�5#      $��
74,309,0,0q�6,   $	��Top=�5$      $��20��6)   $��65��65   $��1��6@   5%      �� 5'   
   -   5        $ 
btnLimpiar��5'      $��Limpiar��6%   $
��Left=�5(      $��197,309,0,0q�6,   $	��Top=�5)      $��20��6)   $��56��65   $��1��6@   5*      �� 5,   
   -   5         ��Name$ txtCodigoFuente��5,      $
��Left=�5.      $	��185��6)   $��
74,119,0,0q�5/      $	��Top=�6"   $	��558��50      $��1��6   �� 51   
   -   5         ��Name$ lblPosicionCursor��51      $��Línea: 1, Columna: 1��52      $��16(�53      $
��Bold"�54       �� Padding$
 10,2q�55      $��Center=�56      $	��Top=�57      $��1��6$   $��	0,312,0,0q�64   �� 5:   
   -   5        $ btnBuscarPatron��5:      $��Buscar Patrón��6*   $
��Left=�5;      $��497,309,0,0q�6,   $	��Top=�5<      $��1��6)   $��20��69   $��76��6E   5=      5>      5?      