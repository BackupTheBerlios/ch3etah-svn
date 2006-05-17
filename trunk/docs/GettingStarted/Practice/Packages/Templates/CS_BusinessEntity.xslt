<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="text" version="1.0" encoding="UTF-8" indent="no"/>
	
	<!-- Parameters -->
	<xsl:param name="EntityNamespace" select="''"/>
	
	<!-- Template Entry Point -->
	<xsl:template match="Metadata">

namespace <xsl:value-of select="$EntityNamespace"/>
{
		<xsl:choose>
			<xsl:when test="OREntity/@private = 'true'">
	internal </xsl:when><xsl:otherwise>
	public </xsl:otherwise>
		</xsl:choose> class <xsl:value-of select="OREntity/@name"/>
		: <xsl:value-of select="$EntityNamespace"/>.Generated.<xsl:value-of select="OREntity/@name"/>
	{
	
		// Use this class to customize the <xsl:value-of select="OREntity/@name"/> entity...
		
	}
}

	</xsl:template>
</xsl:stylesheet>