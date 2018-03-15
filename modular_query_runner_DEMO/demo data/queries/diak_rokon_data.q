<DATA>
	<COLUMNS>
		<column>
			<!-- type: 0 = text, 1 = int -->
			<name value="Rokon neve" />
			<column_name value="csaladtag_neve" />
			<type value="0" />
		</column>
		<column>
			<name value="Diák neve" />
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
			<name value="Születési hely" />
			<column_name value="szuletesi_hely" />
			<type value="0" />
		</column>
		<column>
			<name value="Személyi szám" />
			<column_name value="szig_szam" />
			<type value="0" />
		</column>
		<column>
			<name value="Osztály" />
			<column_name value="osztaly" />
			<type value="1" />
		</column>
		<column>
			<name value="Magaviselet" />
			<column_name value="magaviselet" />
			<type value="1" />
		</column>
	</COLUMNS>
	<SQL>
		<![CDATA[
			SELECT 
				cs.nev as csaladtag_neve,
				d.*
			FROM
				diakok as d
			LEFT JOIN
				csaladtagok as cs on (d.id = cs.diak)
			WHERE
			(
				d.szuletesi_hely in ('Paks','Szekszárd','Tolna')
				AND 
				d.nem = (SELECT id FROM nemek WHERE nem=1)
			)	
				OR
			(
				substr(d.szig_szam,1,1) in ('2','4')
			)
			ORDER BY
				d.nev ASC
		]]>
	</SQL>
</DATA>