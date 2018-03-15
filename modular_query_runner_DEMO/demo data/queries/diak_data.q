<DATA>
	<COLUMNS>
		<column>
			<!-- type: 0 = text, 1 = int -->
			<name value="Név" />
			<column_name value="nev" />
			<type value="0" />
		</column>
		<column>
			<name value="Családnév" />
			<column_name value="csalad_nev" />
			<type value="0" />
		</column>
		<column>
			<name value="Utónév" />
			<column_name value="uto_nev" />
			<type value="0" />
		</column>
		<column>
			<name value="Anyja neve" />
			<column_name value="anyja_neve" />
			<type value="0" />
		</column>
		<column>
			<name value="Születési dátum" />
			<column_name value="szuletesi_datum" />
			<type value="0" />
		</column>
		<column>
			<name value="SZIG szám" />
			<column_name value="szig_szam" />
			<type value="0" />
		</column>
		<column>
			<name value="Osztály" />
			<column_name value="osztaly" />
			<type value="1" />
		</column>
	</COLUMNS>
	<SQL><![CDATA[
		SELECT 
			nev, 
			csalad_nev, 
			uto_nev, 
			anyja_neve, 
			szuletesi_datum, 
			szig_szam, 
			osztaly
		FROM
			diakok
		WHERE
		(
			id > 10
			AND
			id < 100
		)
			AND
		(
			osztaly > 3
			AND
			osztaly < 8
		)]]>
	</SQL>
</DATA>