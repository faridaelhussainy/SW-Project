   M S B A M L   `   `   `  ����D  @SWproject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:SWproject	SWproject  5   	       SWproject.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:SWproject     5   	     � Title$  Login��5   	   $	��600��6   $	��900��6$   ���#E6F7F5.��5   
   �  � ColumnDefinitions 5   
   �� 5      $
��600* �6   �� 5      $
��430* �6   .+�5   
   � $��0��6   ��5      �� 5       �� ImageSource$N HC:\Users\hp\source\repos\SWproject\SWproject\images\loginbackground2.jpg��6   5      �� 5   
   $��1��6   $	��390��6!   $	��600��6-   $��Right=�6:   $��Center=�6V   
������� �� CornerRadius$ 10��5   $   ._�5      �� $��20q�6   .+�5      �� $��LOGIN��6   $��40(�6)   $
��Bold"�67   ���#BFD9E6$��Center=�5       $��	0,30,0,30q�69   �� 5#      $��Username��6   $��14(�6,   $��SemiBold"�6:   ���#BFD9E6$
��Left=�5$      $��0,0,0,5q�67   �� 5%      -   5        $��UsernameTextBox��5%      $��30��61   $��14(�6=   ���#F5F5F5���#17A89E$��1q�5&   0   $��0,0,0,20q�6D   �� 5)      $��Password��6   $��14(�6,   $��SemiBold"�6:   ���#BFD9E6$
��Left=�5*      $��0,20,0,5q�67   G� 5+      -   5        $��PasswordBox��5+      $��30��61   $��14(�6=   ���#F5F5F5���#17A89E$��1q�5,   4   $��0,0,0,20q�6H   �� 5/      -   5        $��Log In��5/      ���#BFD9E6
�������$��16(�6R   $
��Bold"�50      $��40��6+   $	��100��67   $��Center=�6C   ���#BFD9E6$��0q�51   /   $��	0,40,0,20q�6C   52      53      54       � 55   
   -   5          �Name$ 	MainFrame��55        � NavigationUIVisibility$ Hidden=�6#   56      58      