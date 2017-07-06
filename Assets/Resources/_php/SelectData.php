<?php
	include_once('conect.php');
	
	if(isset($_POST['user_id'])){
		$v_user = (int) $_POST['user_id'];
		$v_query = mysql_query("SELECT big_data FROM comp3_unity_db.t2_game_data WHERE id_user = $v_user");
		$v_query_result = mysql_fetch_array($v_query);
		echo($v_query_result[0]);
	}else{
		echo("SELECTED DATA ... php error 1");
	}
?>