<?php
include_once 'conect_to_db.php';

if(isset($_POST['name'])&&isset($_POST['email'])&&isset($_POST['password'])){
	$v_name = mysql_escape_string($_POST['name']);
	$v_email = mysql_escape_string($_POST['email']);
	$v_pw = mysql_escape_string($_POST['password']);
	
	mysql_query("INSERT INTO db_wd.wd_players(name, email, pass) VALUES ('$v_name', '$v_email', '$v_pw')") or die(mysql_error());
	
	$v_user_id = mysql_insert_id();
	mysql_query("INSERT INTO db_wd.wd_data(player_id) VALUES ($v_user_id)") or die(mysql_error());
	
}
else{
echo "error 1";
}

?>