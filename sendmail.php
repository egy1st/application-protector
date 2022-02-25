<?php
   require_once("global.php");
   require_once("MySQL.php");
      

   $username = $_GET['para1'] ;
   $product = $_GET['para2'] ;
   $email = $_GET['para3'] ;
   $vercode = $_GET['para4'] ;
   
   
     
   
   $message = "Dear $username\nPlease use the following verification code to activate your trial period.\n\nVerification Code is :\t $vercode\n\nThis is an Automated Message, Please Do Not Reply.\n\nMyGoldenSoft.com\nSupport Team" ;
   mail( "$email", "$product Activation", "$message" , "From: activation@mygoldensoft.com" );

?>
