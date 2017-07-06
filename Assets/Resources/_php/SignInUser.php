<?php
include_once 'conect_to_db.php';

if(isset($_POST["name"])&&isset($_POST['password']))
{
	$user_name = mysql_escape_string($_POST["name"]);
	$user_pw = mysql_escape_string($_POST["password"]);
	
	$query = mysql_query("SELECT * FROM db_wd.wd_players WHERE name='$user_name' AND pass = '$user_pw'")or die(mysql_error()) ;
	
	//echo mysql_num_rows($query);
	
	if(mysql_num_rows($query) > 0)
	{
		$query_result = mysql_fetch_array($query);		
		echo($query_result['id']);
	}else{
		echo ("mysql_num_rows(query) <= 0");
	}	
}else{
	echo ("Error: not correct User or password. Pleas try again ...");
}
?>