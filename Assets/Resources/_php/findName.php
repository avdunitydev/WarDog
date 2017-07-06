<?php
include_once 'conect_to_db.php';

if(isset($_POST["name"])){
	$v_name = mysql_escape_string($_POST["name"]);
	$v_query = mysql_query("SELECT * FROM db_wd.wd_players WHERE name = '$v_name'")or die(mysql_error()) ;
	
	echo mysql_num_rows($v_query);
	
}
else{
	echo "error: not Correct user name !";
}


?>