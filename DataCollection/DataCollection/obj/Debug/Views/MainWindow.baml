   M S B A M L   `   `   `  ????I  EDataCollection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:DataCollectionDataCollection  5   	       DataCollection.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:DataCollection     5   	   -   5          ?Name$  MainWindow1??5   	    ? Title$ 
MainWindow??5   	   $	??450??6   $	??800??6)   .??5	      ? .+?5
   
   ?? -   5         ??Name$ Capture_Button??5
      $??Capture??5      $??Right=?5      $??55??6-   $??	0,0,10,10q?69   $??Bottom=?6L   $	??216??6g   ??5      ?? *CaptureCommand+5   g   ?? 5   
   -   5         ??Name$ Capture_Image??5      $
??Left=?5      $	??166??6+   $??	10,10,0,0q?68   $	??Top=?6K   $	??324??6c   ??5      ?? *CapturedImageLocation+5   c   ? 5   
   $??Right=?5      $??92??6+   $??	0,10,10,0q?67   $	??Top=?6J   $	??197??6b    ? DataContext 5      ?? *Orientation+.+?5      ?? $9??3X: {Binding X, UpdateSourceTrigger=PropertyChanged}??6   $
??Left=?5      $??30??61   $??22,0,0,0q?6=    ?? TextWrapping$
 Wrap=?6O   $	??Top=?6c   $	??165??6{   ?? 5      $9??3Y: {Binding Y, UpdateSourceTrigger=PropertyChanged}??6   $
??Left=?5      $??30??61   $??	22,29,0,0q?6=   $
 Wrap=?6P   $	??Top=?6d   $	??165??6|   ?? 5      $9??3Z: {Binding Z, UpdateSourceTrigger=PropertyChanged}??6   $
??Left=?5      $??30??61   $??	22,59,0,0q?6=   $
 Wrap=?6P   $	??Top=?6d   $	??165??6|   5      ?? 5   
   -   5         ??Name$ SubjectSelection_ComboBox??5       ?? Text$ Subject??66   $
??Left=?6E   $??55??6`   $??	10,0,0,10q?6l   $??Bottom=?6   $	??366??6?   5      5      