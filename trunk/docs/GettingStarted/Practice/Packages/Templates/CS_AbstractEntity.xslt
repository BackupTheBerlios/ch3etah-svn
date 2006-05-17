<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="text" version="1.0" encoding="UTF-8" indent="no"/>
	
	<!-- Parameters -->
	<xsl:param name="EntityNamespace" select="''"/>
	
	<!-- Template Entry Point -->
	<xsl:template match="Metadata">
	
using System;
using System.Collections;
using System.ComponentModel;

namespace <xsl:value-of select="$EntityNamespace"/>.Generated
{
		<xsl:choose>
			<xsl:when test="OREntity/@private = 'true'">
	internal </xsl:when><xsl:otherwise>
	public </xsl:otherwise>
		</xsl:choose>abstract class <xsl:value-of select="OREntity/@name"/>
	{
	
		#region Constructor <!--
			--><xsl:apply-templates mode="GenerateConstructor" select="OREntity" />
        #endregion Constructor 
		
		#region Properties 
			<xsl:apply-templates mode="GenerateProperties" select="OREntity" />
		#endregion Properties 
		
		#region Associations 
			<xsl:apply-templates mode="GenerateLinks" select="OREntity" />
		#endregion Associations 
		
	}
}
	</xsl:template>
	
	
	
	
	<!-- 
	=======================================================================================
	Helper Templates
	=======================================================================================
	-->
<!--
	<xsl:template mode="GenerateStruct" match="Metadata/OREntity">
	    [Serializable]
        public struct <xsl:value-of select="@name"/>Data
        {
            <xsl:for-each select="Fields/Field">
				internal <xsl:value-of select="@type"/><![CDATA[ ]]><xsl:value-of select="@name"/>;
            </xsl:for-each>
        }
	</xsl:template>
-->
	<xsl:template mode="GenerateConstructor" match="Metadata/OREntity">
        public <xsl:value-of select="@name"/>()
        {
        	// Put any custom initialization logic here
        	// or in the inheriting class.
        }
	</xsl:template>
	
	
	<xsl:template mode="GenerateFields" match="Metadata/OREntity">
		<xsl:for-each select="Fields/Field">
		protected <xsl:value-of select="@type"/> _<xsl:value-of select="@name"/>;<!--
		--></xsl:for-each>
	</xsl:template>
	
	
	<xsl:template mode="GenerateProperties" match="Metadata/OREntity">
		<xsl:for-each select="Fields/Field">
		#region <xsl:value-of select="@name"/> : <xsl:value-of select="@type"/> 
		public event EventHandler <xsl:value-of select="@name"/>Changed;
		protected virtual void On<xsl:value-of select="@name"/>Changed()
		{
			if (<xsl:value-of select="@name"/>Changed != null)
			{
				<xsl:value-of select="@name"/>Changed(this, EventArgs.Empty);
			}
		}
		
		protected <xsl:value-of select="@type"/> _<xsl:value-of select="@name"/>;
		<xsl:choose>
			<xsl:when test="@browsable = 'false'">
		[Browsable(false)]
			</xsl:when>
			<xsl:otherwise>
		[Browsable(true)
		, Category(@"<xsl:value-of select="@category" />")
		, Description(@"<xsl:value-of select="@description" />")]<!--
		--></xsl:otherwise>
		</xsl:choose>
		
		<xsl:choose>
			<xsl:when test="@hidden = 'true'">
		protected</xsl:when>
			<xsl:otherwise>
		public</xsl:otherwise>
		</xsl:choose><![CDATA[ ]]><xsl:value-of select="@type"/><![CDATA[ ]]><xsl:value-of select="@name"/>
		{
			get { return _<xsl:value-of select="@name"/>; }<!--
			--><xsl:choose>
				<xsl:when test="@readonly = 'false'">
			set
			{
				if (value != _<xsl:value-of select="@name"/>)
				{
					_<xsl:value-of select="@name"/> = value;
					On<xsl:value-of select="@name"/>Changed();
				}
			}<!--
			--></xsl:when>
			</xsl:choose>
		}
		#endregion <xsl:value-of select="@name"/> Property
		</xsl:for-each>
	</xsl:template>
	
	
	<xsl:template mode="GenerateLinks" match="Metadata/OREntity">
		<xsl:for-each select="Links/Link">
		<xsl:variable name="linkType">
			<xsl:call-template name="GetLinkType">
				<xsl:with-param name="targetEntity" select="@targetentity" />
				<xsl:with-param name="isCollection" select="@iscollection" />
			</xsl:call-template>
		</xsl:variable>
		#region <xsl:value-of select="@name"/> 
		public event EventHandler <xsl:value-of select="@name"/>Changed;
		protected virtual void On<xsl:value-of select="@name"/>Changed()
		{
			if (<xsl:value-of select="@name"/>Changed != null)
			{
				<xsl:value-of select="@name"/>Changed(this, EventArgs.Empty);
			}
		}
		
		protected <xsl:value-of select="$linkType"/> _<xsl:value-of select="@name"/>;
		<xsl:choose>
			<xsl:when test="@browsable = 'false'">
		[Browsable(false)]</xsl:when>
			<xsl:otherwise>
		[Browsable(true)
		, Category(@"<xsl:value-of select="@category" />")
		, Description(@"<xsl:value-of select="@description" />")]</xsl:otherwise>
		</xsl:choose>
		
		<xsl:choose>
			<xsl:when test="@hidden = 'true'">
		protected</xsl:when>
			<xsl:otherwise>
		public</xsl:otherwise>
		</xsl:choose><![CDATA[ ]]><xsl:value-of select="$linkType"/><![CDATA[ ]]><xsl:value-of select="@name"/>
		{
			get { return _<xsl:value-of select="@name"/>; }<!--
			--><xsl:choose>
				<xsl:when test="@readonly = 'false'">
			set
			{
				if (value != _<xsl:value-of select="@name"/>)
				{
					_<xsl:value-of select="@name"/> = value;
					On<xsl:value-of select="@name"/>Changed();
				}
			}<!--
			--></xsl:when>
			</xsl:choose>
		}
		#endregion <xsl:value-of select="@name"/> Association
		</xsl:for-each>
	</xsl:template>
	
	<xsl:template name="GetLinkType">
		<xsl:param name="targetEntity" />
		<xsl:param name="isCollection" />
		<xsl:choose>
			<xsl:when test="$isCollection = 'true'">IList</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$EntityNamespace"/>.<xsl:value-of select="$targetEntity"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
<!--
	<xsl:template mode="GenerateMethods" match="Metadata/OREntity">
	
        /// <summary>
        /// CleanUp all Fields
        /// </summary>
        public override void Reset()
        {
            // Reset Data Structs
            this._CurrentData = new <xsl:value-of select="@name"/>Data();
            this._BackupData = new <xsl:value-of select="@name"/>Data();
            
            // Reset Fields
			<xsl:for-each select="Fields/Field">
				 this.Reset<xsl:value-of select="@name"/>();
			</xsl:for-each>
            
            // Mark New
            base.MarkNew();
        }	
	
	</xsl:template>
	
	<xsl:template mode="GenerateIEditableObjectMembers" match="Metadata/OREntity">

	    public override void BeginEdit()
        {
            // Validate Operation
            if (base.Editing) { return; }
            
            // Backup Data
            this._BackupData = this._CurrentData;
            
            // Update Flag
            base.Editing = true;
        }

        public override void CancelEdit()
        {
            // Validate Operation
            if (!base.Editing) { return; }

            // Restore Data
            this._CurrentData = this._BackupData;

            // Update Flag
            base.Editing = false;
        }

        public override void EndEdit()
        {
            // Validate Operation
            if (!base.Editing) { return; }

            // Backup Data
            this._BackupData = new <xsl:value-of select="@name"/>Data();

            // Update Flag
            base.Editing = false;
        }
	
	</xsl:template>
	
	<xsl:template mode="PropertyIndexer" match="Metadata/OREntity">

        public object this[string propertyName]
        {
            get
            {
                switch(propertyName)
                {
					<xsl:for-each select="Fields/Field">
						case "<xsl:value-of select="@name"/>": return this.<xsl:value-of select="@name"/>; break;
					</xsl:for-each>
                    default: return base.GetPropertyByName(propertyName, this); break;
                }
            }
            set
            {
                switch (propertyName)
                {
                	<xsl:for-each select="Fields/Field">
						<xsl:if test="@readonly = 'false'">
							case "<xsl:value-of select="@name"/>": this.<xsl:value-of select="@name"/> = (<xsl:value-of select="@type"/>) value; break;
						</xsl:if>
					</xsl:for-each>
                    default: base.SetPropertyByName(propertyName, value, this); break;
                }
            }
        }

	</xsl:template>	
-->
	
	<xsl:template mode="GenerateEnums" match="Metadata/OREntity">
	
		<xsl:for-each select="Enums/Enum">
			<!-- Create Variables -->
			<xsl:variable name="Separator" select="','" />
			
			<!-- Method -->
			public enum <xsl:value-of select="@name"/>
			{
				<xsl:for-each select="ValueList/ListItem">
					<xsl:value-of select="@key"/> = <xsl:value-of select="@value"/><xsl:value-of select="$Separator"/>
				</xsl:for-each>
			}
		</xsl:for-each>
	
	</xsl:template>	
	
</xsl:stylesheet>
