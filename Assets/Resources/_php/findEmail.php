<?php
include_once 'conect_to_db.php';

if(isset($_POST["email"])){
	$v_email = mysql_escape_string($_POST["email"]);
	$v_query = mysql_query("SELECT * FROM db_wd.wd_players WHERE email='$v_email'")or die(mysql_error()) ;
	
	echo mysql_num_rows($v_query);
	
}
else{
	echo "error: not Correct user e-mail !";
}


?>