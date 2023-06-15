function Login() {
	var Uid = $("#txtUsername").val();
	var pwd = $("#password-field").val();
	var userType = $('#ddluserType option:selected').val();
	$.ajax({
		type: "GET",
		url: 'Login/SaveLogin',
		
		contentType: "application/json; charset=utf-8",
		data: { "UserName": Uid, "Password": pwd, "UserType": userType },
		dataType: "json"
	}).done(function (data) {
		alert(data);
	})
}