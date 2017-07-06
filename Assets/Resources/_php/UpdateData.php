<?php
include_once 'conect.php';
 
if(isset($_POST['user_id']) && isset($_POST['data'])){
	$v_user_id = (int) $_POST['user_id'];
	$v_data = mysql_escape_string($_POST['data']);
		
	mysql_query("UPDATE comp3_unity_db.t2_game_data SET big_data = '$v_data' WHERE id_user = $v_user_id");
}
else{
echo "UPDATE DATA ... php error 1";
}
?>
