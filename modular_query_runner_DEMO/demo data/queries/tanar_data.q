<DATA>
	<COLUMNS>
		<column>
			<!-- type: 0 = text, 1 = int -->
			<name value="Név" />
			<column_name value="nev" />
			<type value="0" />
		</column>
		<column>
			<name value="Nem" />
			<column_name value="nem" />
			<type value="1" />
		</column>
		<column>
			<name value="Osztályfőnök" />
			<column_name value="osztaly_fonok" />
			<type value="1" />
		</column>
	</COLUMNS>
	<SQL>
		<![CDATA[
			SELECT 
				*
			FROM
				tanarok
			WHERE
				nem = (SELECT id FROM nemek WHERE nem='Nő' )
			ORDER BY
				nev ASC
		]]>
	</SQL>
</DATA>