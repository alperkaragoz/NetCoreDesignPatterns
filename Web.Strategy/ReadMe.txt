
<------------------- Strategy Design Pattern ------------------->

	* Runtime sırasında objenin davranışını değiştirmemize olanak sağlar.Örn: Kullanıcıya opsiyon sunar.Kullanıcıya A->Z'ye mi, Z>A'ya mı sıralama yapmak istediğini seçmesine olanak tanır.
	* Behavioral (Davranışsal) kategorisindedir.
	* Consumer'lara runtime sırasında strategy(algoritma) seçmelerine olanak sağlar.
	* Claim'ler kullanıcılar hakkında tuttuğumuz ek datalardır.Cookielerde de tutulur.Bu sayede claim bazlı policy bazlı yetkilendirme yapabiliyoruz.
	* => classlarda propertynin get i olan seti olmayana karşılık gelir.
	* 