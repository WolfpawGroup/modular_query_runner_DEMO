<DATA>
	<COLUMNS>
		<column>
			<name value="Diák" />
			<column_name value="diak" />
			<type value="0" />
		</column>
		<column>
			<name value="Óra" />
			<column_name value="ora" />
			<type value="0" />
		</column>
		<column>
			<!-- type: 0 = text, 1 = int -->
			<name value="Átlag jegy" />
			<column_name value="atlag" />
			<type value="0" />
		</column>
	</COLUMNS>
	<SQL>
		<![CDATA[
			SELECT 
				substr(round(avg(n.jegy),2) || '0', 1,4) as atlag,
				(SELECT ora_neve FROM orak WHERE id=n.ora) as ora,
				(SELECT nev FROM diakok WHERE id=n.diak) as diak				
			FROM
				naplo_bejegyzesek as n
			WHERE
				((SELECT avg(jegy) FROM naplo_bejegyzesek as nn WHERE nn.id = n.id) < 3 )
			GROUP BY
				n.diak,
				n.ora
			order by 
				(SELECT nev FROM diakok where id=n.diak)
		]]>
	</SQL>
</DATA>