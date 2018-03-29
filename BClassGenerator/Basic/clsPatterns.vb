
'Copyright 2018 Kelvys B. Pantaleão

'This file is part of BClassGenerator

'BGlobal Is free software: you can redistribute it And/Or modify
'it under the terms Of the GNU General Public License As published by
'the Free Software Foundation, either version 3 Of the License, Or
'(at your option) any later version.

'This program Is distributed In the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty Of
'MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License For more details.

'You should have received a copy Of the GNU General Public License
'along with this program.  If Not, see <http://www.gnu.org/licenses/>.


'Este arquivo é parte Do programa BClassGenerator

'BGlobal é um software livre; você pode redistribuí-lo e/ou 
'modificá-lo dentro dos termos da Licença Pública Geral GNU como 
'publicada pela Fundação Do Software Livre (FSF); na versão 3 da 
'Licença, ou(a seu critério) qualquer versão posterior.

'Este programa é distribuído na esperança de que possa ser  útil, 
'mas SEM NENHUMA GARANTIA; sem uma garantia implícita de ADEQUAÇÃO
'a qualquer MERCADO ou APLICAÇÃO EM PARTICULAR. Veja a
'Licença Pública Geral GNU para maiores detalhes.

'Você deve ter recebido uma cópia da Licença Pública Geral GNU junto
'com este programa, Se não, veja <http://www.gnu.org/licenses/>.

'GitHub: https://github.com/Kelvysb/BClassGenerator

Imports Newtonsoft
Imports BGlobal
Imports BDataBase
Imports Newtonsoft.Json
Imports BClassGenerator

Public Class clsPattern
    Implements IPatterns

#Region "Declarations"
    Protected strName As String
    Private strBaseName As String
    Protected enmTargetLanguage As enm_OutputLanguage
    Protected objPaternFiles As List(Of clsPaternFile)
    Protected objPaternGroups As List(Of clsPaternGroup)
    Protected objPaternItens As List(Of clsPaternItem)

    Protected objTableInfo As clsTableInfo
    Protected objSelectedIndex As clsIndexInfo
    Protected objSelectedField As clsColumnInfo
#End Region

#Region "Constructor"
    Public Sub New(p_xmlInput As XDocument, p_objTableInfo As clsTableInfo)
        Try
            Call sbfromXml(p_xmlInput)
            objTableInfo = p_objTableInfo
            objSelectedIndex = Nothing
            objSelectedField = Nothing
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub New(p_objTableInfo As clsTableInfo)
        Try
            strName = ""
            strBaseName = ""
            enmTargetLanguage = enm_OutputLanguage.VbNet
            objPaternItens = New List(Of clsPaternItem)

            'Types
            objPaternItens.Add(New clsPaternItem("#STRINGTYPE;", "String"))
            objPaternItens.Add(New clsPaternItem("#XMLTYPE;", "Xdocument"))
            objPaternItens.Add(New clsPaternItem("#NUMBERTYPE;", "Double"))
            objPaternItens.Add(New clsPaternItem("#INTEGERTYPE;", "Integer"))
            objPaternItens.Add(New clsPaternItem("#FILETYPE;", "Object"))
            objPaternItens.Add(New clsPaternItem("#DATETYPE;", "Date"))
            objPaternItens.Add(New clsPaternItem("#BOOLEANTYPE;", "Boolean"))
            objPaternItens.Add(New clsPaternItem("#OBJECTTYPE;", "Object"))
            objPaternItens.Add(New clsPaternItem("#STRINGPREFIX;", "str"))
            objPaternItens.Add(New clsPaternItem("#XMLPREFIX;", "xml"))
            objPaternItens.Add(New clsPaternItem("#NUMBERPREFIX;", "dbl"))
            objPaternItens.Add(New clsPaternItem("#INTEGERPREFIX;", "int"))
            objPaternItens.Add(New clsPaternItem("#FILEPREFIX;", "obj"))
            objPaternItens.Add(New clsPaternItem("#DATEPREFIX;", "dte"))
            objPaternItens.Add(New clsPaternItem("#BOOLEANPREFIX;", "bln"))
            objPaternItens.Add(New clsPaternItem("#OBJECTPREFIX;", "obj"))
            objPaternItens.Add(New clsPaternItem("#STRINGINITIALIZER;", """"""))
            objPaternItens.Add(New clsPaternItem("#XMLINITIALIZER;", "New Xdocument()"))
            objPaternItens.Add(New clsPaternItem("#NUMBERINITIALIZER;", "0"))
            objPaternItens.Add(New clsPaternItem("#INTEGERINITIALIZER;", "0"))
            objPaternItens.Add(New clsPaternItem("#FILEINITIALIZER;", "Nothing"))
            objPaternItens.Add(New clsPaternItem("#DATEINITIALIZER;", "Now()"))
            objPaternItens.Add(New clsPaternItem("#BOOLEANINITIALIZER;", "False"))
            objPaternItens.Add(New clsPaternItem("#OBJECTINITIALIZER;", "Nothing"))


            'Field Converters
            objPaternItens.Add(New clsPaternItem("#TOSTRING_XML_PREFIX;", ""))
            objPaternItens.Add(New clsPaternItem("#TOSTRING_XML_SUFIX;", ".ToString"))

            objPaternItens.Add(New clsPaternItem("#TOSTRING_NUMBER_PREFIX;", ""))
            objPaternItens.Add(New clsPaternItem("#TOSTRING_NUMBER_SUFIX;", ".ToString"))

            objPaternItens.Add(New clsPaternItem("#TOSTRING_INTEGER_PREFIX;", ""))
            objPaternItens.Add(New clsPaternItem("#TOSTRING_INTEGER_SUFIX;", ".ToString"))

            objPaternItens.Add(New clsPaternItem("#TOSTRING_DATE_PREFIX;", ""))
            objPaternItens.Add(New clsPaternItem("#TOSTRING_DATE_SUFIX;", ".ToString"))

            objPaternItens.Add(New clsPaternItem("#TOSTRING_BOOLEAN_PREFIX;", ""))
            objPaternItens.Add(New clsPaternItem("#TOSTRING_BOOLEAN_SUFIX;", ".ToString"))


            objPaternItens.Add(New clsPaternItem("#FROMSTRING_XML_PREFIX;", "Xdocument.Parse("))
            objPaternItens.Add(New clsPaternItem("#FROMSTRING_XML_SUFIX;", ")"))

            objPaternItens.Add(New clsPaternItem("#FROMSTRING_NUMBER_PREFIX;", "Double.Parse("))
            objPaternItens.Add(New clsPaternItem("#FROMSTRING_NUMBER_SUFIX;", ")"))

            objPaternItens.Add(New clsPaternItem("#FROMSTRING_INTEGER_PREFIX;", "Integer.Parse("))
            objPaternItens.Add(New clsPaternItem("#FROMSTRING_INTEGER_SUFIX;", ")"))

            objPaternItens.Add(New clsPaternItem("#FROMSTRING_DATE_PREFIX;", "Date.Parse("))
            objPaternItens.Add(New clsPaternItem("#FROMSTRING_DATE_SUFIX;", ")"))

            objPaternItens.Add(New clsPaternItem("#FROMSTRING_BOOLEAN_PREFIX;", "Boolean.Parse("))
            objPaternItens.Add(New clsPaternItem("#FROMSTRING_BOOLEAN_SUFIX;", ")"))





            'Class
            objPaternItens.Add(New clsPaternItem("#ABSTRACTPREFIX;", "abs"))
            objPaternItens.Add(New clsPaternItem("#BASICCLASSPREFIX;", "bas"))
            objPaternItens.Add(New clsPaternItem("#BUSINESSPREFIX;", "neg"))
            objPaternItens.Add(New clsPaternItem("#CLASSPREFIX;", "cls"))
            objPaternItens.Add(New clsPaternItem("#FACTORYPREFIX;", "fac"))
            objPaternItens.Add(New clsPaternItem("#INTERFACEPREFIX;", "I"))
            objPaternItens.Add(New clsPaternItem("#REPOSITORYPREFIX;", "rep"))



            'Regions
            objPaternItens.Add(New clsPaternItem("#OPENCONSTRUCTORREGION;", "#REGION ""Constructors"""))
            objPaternItens.Add(New clsPaternItem("#CLOSECONSTRUCTORREGION;", "#END REGION"))
            objPaternItens.Add(New clsPaternItem("#OPENDECLARATIONSREGION;", "#REGION ""Declarations"""))
            objPaternItens.Add(New clsPaternItem("#CLOSEDECLARATIONSREGION;", "#END REGION"))
            objPaternItens.Add(New clsPaternItem("#OPENFUNCTIONSREGION;", "#REGION ""Functions"""))
            objPaternItens.Add(New clsPaternItem("#CLOSEFUNCTIONSREGION;", "#END REGION"))
            objPaternItens.Add(New clsPaternItem("#OPENPROPERTIESREGION;", "#REGION ""Properties"""))
            objPaternItens.Add(New clsPaternItem("#CLOSEPROPERTIESREGION;", "#END REGION"))

            'Functons Subroutines indicators
            objPaternItens.Add(New clsPaternItem("#FUNCTIONPREFIX;", "fn"))
            objPaternItens.Add(New clsPaternItem("#SUBROUTINEPREFIX;", "sb"))
            objPaternItens.Add(New clsPaternItem("#FUNCTIONINDICATOR;", "Function"))
            objPaternItens.Add(New clsPaternItem("#SUBROUTINEINDICATOR;", "Sub"))


            'Class Indicators
            objPaternItens.Add(New clsPaternItem("#CLASSINDICATOR;", "Class"))
            objPaternItens.Add(New clsPaternItem("#INTERFACEINDICATOR;", "Interface"))
            objPaternItens.Add(New clsPaternItem("#PROPERTYINDICATOR;", "Property"))
            objPaternItens.Add(New clsPaternItem("#IMPLEMENTSINDICATOR;", "Implements"))
            objPaternItens.Add(New clsPaternItem("#INHERITSINDICATOR;", "Inherits"))


            'Blocks
            objPaternItens.Add(New clsPaternItem("#OPENFUNCTION;", "Function"))
            objPaternItens.Add(New clsPaternItem("#CLOSEFUNCTION;", "End Function"))
            objPaternItens.Add(New clsPaternItem("#OPENSUBROUTINE;", "Sub"))
            objPaternItens.Add(New clsPaternItem("#CLOSESUBROUTINE;", "End Sub"))
            objPaternItens.Add(New clsPaternItem("#OPENPARAMETERS;", "("))
            objPaternItens.Add(New clsPaternItem("#CLOSEPARAMETERS;", ")"))
            objPaternItens.Add(New clsPaternItem("#OPENCLASS;", "Class"))
            objPaternItens.Add(New clsPaternItem("#CLOSECLASS;", "End Class"))
            objPaternItens.Add(New clsPaternItem("#OPENPROPERTY;", "Property"))
            objPaternItens.Add(New clsPaternItem("#CLOSEPROPERTY;", "End Property"))
            objPaternItens.Add(New clsPaternItem("#OPENINTERFACE;", "Interface"))
            objPaternItens.Add(New clsPaternItem("#CLOSEINTERFACE;", "End Interface"))
            objPaternItens.Add(New clsPaternItem("#OPENBLOCK;", ""))
            objPaternItens.Add(New clsPaternItem("#CLOSEBLOCK;", ""))
            objPaternItens.Add(New clsPaternItem("#OPENTRY;", "Try"))
            objPaternItens.Add(New clsPaternItem("#CLOSETRY;", "End Try"))
            objPaternItens.Add(New clsPaternItem("#OPENCATCH;", "Catch ex As Exception"))
            objPaternItens.Add(New clsPaternItem("#CLOSECATCH;", "End Catch"))
            objPaternItens.Add(New clsPaternItem("#OPENIF;", "If"))
            objPaternItens.Add(New clsPaternItem("#CLOSEIF;", "End If"))
            objPaternItens.Add(New clsPaternItem("#OPENGET;", "Get"))
            objPaternItens.Add(New clsPaternItem("#CLOSEGET;", "End Get"))
            objPaternItens.Add(New clsPaternItem("#OPENSET;", "Set"))
            objPaternItens.Add(New clsPaternItem("#CLOSESET;", "End Set"))


            'Access
            objPaternItens.Add(New clsPaternItem("#PUBLICACCESS;", "Public"))
            objPaternItens.Add(New clsPaternItem("#DEFAULTACCESS;", ""))
            objPaternItens.Add(New clsPaternItem("#PRIVATEACCESS;", "Private"))
            objPaternItens.Add(New clsPaternItem("#PROTECTEDACCESS;", "Protected"))
            objPaternItens.Add(New clsPaternItem("#FRIENDACCESS;", "Friend"))

            'Modifiers
            objPaternItens.Add(New clsPaternItem("#STATICMODIFIER;", "Shared"))
            objPaternItens.Add(New clsPaternItem("#READONLYMODIFIER;", "ReadOnly"))
            objPaternItens.Add(New clsPaternItem("#OVERLOADMODIFIER;", "Overloads"))
            objPaternItens.Add(New clsPaternItem("#OVERRIDEMODIFIER;", "Overrides"))
            objPaternItens.Add(New clsPaternItem("#OVERLOADABLEMODIFIER;", "Overloadable"))
            objPaternItens.Add(New clsPaternItem("#OVERRIDABLEMODIFIER;", "Overridable"))
            objPaternItens.Add(New clsPaternItem("#ABSTRACTMODIFIER;", "MustInherit"))
            objPaternItens.Add(New clsPaternItem("#FINALMODIFIER;", "Final"))

            'Others
            objPaternItens.Add(New clsPaternItem("#CLASSFILEEXTENSSION;", ".vb"))
            objPaternItens.Add(New clsPaternItem("#GROUPFILEEXTENSSION;", ".vb"))
            objPaternItens.Add(New clsPaternItem("#DEFAULTTHROW;", "Throw"))
            objPaternItens.Add(New clsPaternItem("#PARAMETERPREFIX;", "p_"))

            objPaternItens.Add(New clsPaternItem("#SIMPLECOMMENTPREFIX;", "'"))
            objPaternItens.Add(New clsPaternItem("#BLOCKCOMMENTPREFIX;", "'"))
            objPaternItens.Add(New clsPaternItem("#SIMPLECOMMENTSUFIX;", ""))
            objPaternItens.Add(New clsPaternItem("#BLOCKCOMMENTSUFIX;", ""))


            'Groups
            objPaternGroups = New List(Of clsPaternGroup)
            objPaternGroups.Add(New clsPaternGroup("#BASICCLASS;", {
                                                   "#PUBLICACCESS;", "#SPACE;", "#CLASSINDICATOR;", "#SPACE;", "#BASICCLASSPREFIX;", "#BASE_NAME;", "#NEWLINE;", "#NEWLINE;",
                                                   "#DECLARATIONGROUP;", "#NEWLINE;", "#NEWLINE;",
                                                   "#CONSTRUCTORGROUP;", "#NEWLINE;", "#NEWLINE;",
                                                   "#FUNCTIONSGROUP;", "#NEWLINE;", "#NEWLINE;",
                                                   "#PROPERTIESGROUP;", "#NEWLINE;",
                                                   "#CLOSECLASS;", "#NEWLINE;"}.ToList))


            objPaternGroups.Add(New clsPaternGroup("#DECLARATIONGROUP;", {
                                                    "#OPENDECLARATIONSREGION;", "#NEWLINE;",
                                                    "#FOREACH_FIELD;>#DECLARE_FIELD;",
                                                    "#CLOSECONSTRUCTORREGION;"}.ToList))

            objPaternGroups.Add(New clsPaternGroup("#CONSTRUCTORGROUP;", {
                                                    "#OPENCONSTRUCTORREGION;", "#NEWLINE;",
                                                    "#TAB;", "#PUBLICACCESS;", "#SPACE;", "#SUBROUTINEINDICATOR;", "#SPACE;", "New()", "#NEWLINE;",
                                                    "#TAB;", "#TAB;", "#OPENTRY;", "#NEWLINE;",
                                                    "#FOREACH_FIELD;>#INITIALIZE_FIELD;",
                                                    "#CATCHGROUP;", "#NEWLINE;",
                                                    "#TAB;", "#CLOSESUBROUTINE;", "#NEWLINE;", "#NEWLINE;",
                                                    "#TAB;", "#PUBLICACCESS;", "#SPACE;", "#SUBROUTINEINDICATOR;", "#SPACE;", "New(", "#PARAMETERPREFIX;", "#XMLPREFIX;", "Input As ", "#XMLTYPE;", ")", "#NEWLINE;",
                                                    "#TAB;", "#TAB;", "#OPENTRY;", "#NEWLINE;",
                                                    "#TAB;", "#TAB;", "#TAB;", "Call fromXml(", "#PARAMETERPREFIX;", "#XMLPREFIX;", "Input)", "#NEWLINE;",
                                                    "#CATCHGROUP;", "#NEWLINE;",
                                                    "#TAB;", "#CLOSESUBROUTINE;", "#NEWLINE;",
                                                    "#CLOSECONSTRUCTORREGION;"}.ToList))

            objPaternGroups.Add(New clsPaternGroup("#FUNCTIONSGROUP;", {
                                                    "#OPENFUNCTIONSREGION;", "#NEWLINE;",
                                                    "#TAB;", "#PUBLICACCESS;", "#SPACE;", "#OVERRIDABLEMODIFIER;", "#SPACE;", "#FUNCTIONINDICATOR;", "#SPACE;", "toXml() As ", "#XMLTYPE;", "#NEWLINE;", "#NEWLINE;",
                                                    "#TAB;", "#TAB;", "Dim ", "#XMLPREFIX;", "Return As ", "#XMLTYPE;", "#NEWLINE;", "#NEWLINE;",
                                                    "#TAB;", "#TAB;", "#OPENTRY;", "#NEWLINE;",
                                                    "#TAB;", "#TAB;", "#TAB;", "#XMLPREFIX;", "Return = ", "New ", "#XMLTYPE;", "(<", "#BASE_NAME;", "></", "#BASE_NAME;", ">", ")", "#NEWLINE;", "#NEWLINE;",
                                                    "#FOREACH_FIELD;>#TOXML_FIELD;", "#NEWLINE;",
                                                    "Return ", "#XMLPREFIX;", "Return", "#NEWLINE;",
                                                    "#CATCHGROUP;", "#NEWLINE;",
                                                    "#TAB;", "#CLOSEFUNCTION;", "#NEWLINE;", "#NEWLINE;",
                                                    "#TAB;", "#PROTECTEDACCESS;", "#SPACE;", "#OVERRIDABLEMODIFIER;", "#SPACE;", "#SUBROUTINEINDICATOR;", "#SPACE;", "fromXml(", "#PARAMETERPREFIX;", "#XMLPREFIX;", "Input As ", "#XMLTYPE;", ")", "#NEWLINE;", "#NEWLINE;",
                                                    "#TAB;", "#TAB;", "#OPENTRY;", "#NEWLINE;",
                                                    "#FOREACH_FIELD;>#FROMXML_FIELD;", "#NEWLINE;",
                                                    "#CATCHGROUP;", "#NEWLINE;",
                                                    "#TAB;", "#CLOSESUBROUTINE;", "#NEWLINE;", "#NEWLINE;",
                                                    "#TAB;", "#PUBLICACCESS;", "#SPACE;", "#OVERRIDABLEMODIFIER;", "#SPACE;", "#FUNCTIONINDICATOR;", "#SPACE;", "clone() As ", "#BASICCLASSPREFIX;", "#BASE_NAME;", "#NEWLINE;", "#NEWLINE;",
                                                    "#TAB;", "#TAB;", "#OPENTRY;", "#NEWLINE;",
                                                    "Return New ", "#BASICCLASSPREFIX;", "#BASE_NAME;", "(toXml)", "#NEWLINE;",
                                                    "#CATCHGROUP;", "#NEWLINE;",
                                                    "#TAB;", "#CLOSEFUNCTION;", "#NEWLINE;",
                                                    "#CLOSEFUNCTIONSREGION;"}.ToList))


            objPaternGroups.Add(New clsPaternGroup("#PROPERTIESGROUP;", {
                                                    "#OPENPROPERTIESREGION;", "#NEWLINE;",
                                                    "#FOREACH_FIELD;>#PROPERTY_FIELD;", "#NEWLINE;",
                                                    "#CLOSEPROPERTIESREGION;"}.ToList))


            objPaternGroups.Add(New clsPaternGroup("#VARIABLE_NAME;", {"#FIELD_PREFIX;", "#NORMALIZE;>#FIELD_NAME;"}.ToList))
            objPaternGroups.Add(New clsPaternGroup("#DECLARE_FIELD;", {"#TAB;", "#PROTECTEDACCESS;", "#SPACE;", "#VARIABLE_NAME;", "#SPACE;", " As ", "#FIELD_TYPE;", "#NEWLINE;"}.ToList))
            objPaternGroups.Add(New clsPaternGroup("#INITIALIZE_FIELD;", {"#TAB;", "#TAB;", "#TAB;", "#VARIABLE_NAME;", "#SPACE;", " = ", "#FIELD_INITIALIZER;", "#NEWLINE;"}.ToList))

            objPaternGroups.Add(New clsPaternGroup("#TOXML_FIELD;", {"#TAB;", "#TAB;", "#TAB;", "#XMLPREFIX;", "Return.<", "#BASE_NAME;", ">.First.Add(<", "#FIELD_NAME;", "><%=", "#FIELD_TOSTRING;>#VARIABLE_NAME;", "#SPACE;", "%></", "#FIELD_NAME;", ">)", "#NEWLINE;"}.ToList))
            objPaternGroups.Add(New clsPaternGroup("#FROMXML_FIELD;", {"#TAB;", "#TAB;", "#TAB;", "#VARIABLE_NAME;", " = ", "#FIELD_FROMSTRING;>#GETXML_FIELD;", "#NEWLINE;"}.ToList))
            objPaternGroups.Add(New clsPaternGroup("#GETXML_FIELD;", {"#PARAMETERPREFIX;", "#XMLPREFIX;", "Input.<", "#BASE_NAME;", ">.<", "#FIELD_NAME;", ">.Value"}.ToList))

            objPaternGroups.Add(New clsPaternGroup("#PROPERTY_FIELD;", {
                                                   "#TAB;", "#PUBLICACCESS;", "#SPACE;", "#PROPERTYINDICATOR;", "#SPACE;", "#NORMALIZE;>#FIELD_NAME;", " As ", "#FIELD_TYPE;", "#NEWLINE;",
                                                   "#TAB;", "#TAB;", "#OPENGET;", "#NEWLINE;",
                                                   "#TAB;", "#TAB;", "#TAB;", "Return ", "#VARIABLE_NAME;", "#NEWLINE;",
                                                   "#TAB;", "#TAB;", "#CLOSEGET;", "#NEWLINE;",
                                                   "#TAB;", "#TAB;", "#OPENSET;", "(value As ", "#FIELD_TYPE;", ")", "#NEWLINE;",
                                                   "#TAB;", "#TAB;", "#TAB;", "#VARIABLE_NAME;", " = value", "#NEWLINE;",
                                                   "#TAB;", "#TAB;", "#CLOSESET;", "#NEWLINE;",
                                                   "#TAB;", "#CLOSEPROPERTY;", "#NEWLINE;", "#NEWLINE;"}.ToList))

            objPaternGroups.Add(New clsPaternGroup("#CATCHGROUP;", {
                                                    "#TAB;", "#TAB;", "#OPENCATCH;", "#NEWLINE;",
                                                    "#TAB;", "#TAB;", "#TAB;", "#DEFAULTTHROW;", "#NEWLINE;",
                                                    "#TAB;", "#TAB;", "#CLOSETRY;"}.ToList))

            'Files
            objPaternFiles = New List(Of clsPaternFile)
            objPaternFiles.Add(New clsPaternFile("Basic", {"#BASICCLASSPREFIX;", "#BASE_NAME;", "#CLASSFILEEXTENSSION;"}.ToList, {"#BASICCLASS;"}.ToList))


            objTableInfo = p_objTableInfo

            objSelectedIndex = Nothing
            objSelectedField = Nothing

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub New()
        Call Me.New(Nothing)
    End Sub
#End Region

#Region "Functions"
    Public Overridable Function toXml() As XDocument

        Dim xmlReturn As XDocument

        Try

            xmlReturn = New XDocument(<Pattern>
                                          <TargetLanguage><%= enmTargetLanguage.ToString %></TargetLanguage>
                                          <Name><%= strName.Trim %></Name>
                                      </Pattern>)


            xmlReturn.<Pattern>.First.Add(<PaternFiles></PaternFiles>)
            For Each File As clsPaternFile In objPaternFiles
                xmlReturn.<Pattern>.<PaternFiles>.First.Add(File.toXml.Elements)
            Next

            xmlReturn.<Pattern>.First.Add(<PaternGroups></PaternGroups>)
            For Each Group As clsPaternGroup In objPaternGroups
                xmlReturn.<Pattern>.<PaternGroups>.First.Add(Group.toXml.Elements)
            Next

            xmlReturn.<Pattern>.First.Add(<PaternItens></PaternItens>)
            For Each Item As clsPaternItem In objPaternItens
                xmlReturn.<Pattern>.<PaternItens>.First.Add(Item.toXml.Elements)
            Next

            Return xmlReturn

        Catch ex As Exception
            Throw
        End Try
    End Function

    Protected Overridable Sub sbfromXml(p_xmlInput As XDocument)

        Dim i As Integer

        Try

            [Enum].TryParse(Of enm_OutputLanguage)(p_xmlInput.<Pattern>.<TargetLanguage>.Value, enmTargetLanguage)
            strName = p_xmlInput.<Pattern>.<Name>.Value.Trim

            objPaternFiles = New List(Of clsPaternFile)
            For Each Item As XElement In p_xmlInput.<Patern>.<PaternFiles>.Elements
                objPaternFiles.Add(New clsPaternFile(New XDocument(Item)))
            Next

            objPaternGroups = New List(Of clsPaternGroup)
            For Each Item As XElement In p_xmlInput.<Patern>.<PaternGroups>.Elements
                PaternGroups.Add(New clsPaternGroup(New XDocument(Item)))
            Next

            objPaternItens = New List(Of clsPaternItem)
            For Each Item As XElement In p_xmlInput.<Patern>.<PaternItens>.Elements
                objPaternItens.Add(New clsPaternItem(New XDocument(Item)))
            Next

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Overridable Function clone() As IPatterns
        Try
            Return New clsPattern(toXml, objTableInfo.clone)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Overridable Function serialize() As String
        Try
            Return Json.JsonConvert.SerializeObject(Me, Json.Formatting.Indented)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Shared Function serialize(p_objInput As IPatterns) As String
        Try
            Return Json.JsonConvert.SerializeObject(p_objInput, Json.Formatting.Indented)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Shared Function deserialize(p_strJson As String) As IPatterns
        Try
            Return Json.JsonConvert.DeserializeObject(Of IPatterns)(p_strJson)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Protected Overridable Function fnGetSpecialKeys(p_strKey As String) As String

        Dim strReturn As String
        Dim strAuxSubKey As List(Of String)
        Dim objAuxKeys As List(Of clsColumnInfo)

        Try

            strReturn = ""

            If p_strKey.Trim.ToUpper.StartsWith("#FOREACH_FIELD;") Then
                strAuxSubKey = p_strKey.Split(">").ToList

                For Each Field As clsColumnInfo In objTableInfo.Columns
                    objSelectedField = Field
                    strReturn = strReturn & fnGetKey(strAuxSubKey(1))
                Next

                objSelectedField = Nothing

            ElseIf p_strKey.Trim.ToUpper.StartsWith("#FOREACH_KEYFIELD;") Then
                strAuxSubKey = p_strKey.Split(">").ToList
                objAuxKeys = (From Column As clsColumnInfo In objTableInfo.Columns
                              Where Column.PrimaryKey = True
                              Select Column).ToList

                For Each Field As clsColumnInfo In objAuxKeys
                    objSelectedField = Field
                    strReturn = strReturn & fnGetKey(strAuxSubKey(1))
                Next

                objSelectedField = Nothing


            ElseIf p_strKey.Trim.ToUpper.StartsWith("#IF_LASTFIELD;") Then
                strAuxSubKey = p_strKey.Split(">").ToList

                If objSelectedField Is objTableInfo.Columns.Last Then
                    strReturn = strReturn & fnGetKey(strAuxSubKey(1))
                Else
                    If strAuxSubKey.Count >= 4 AndAlso strAuxSubKey(2).Trim.ToUpper = "#ELSE;" Then
                        strReturn = strReturn & fnGetKey(strAuxSubKey(3))
                    End If
                End If

            ElseIf p_strKey.Trim.ToUpper.StartsWith("#IF_FIRSTFIELD;") Then
                strAuxSubKey = p_strKey.Split(">").ToList

                If objSelectedField Is objTableInfo.Columns.First Then
                    strReturn = strReturn & fnGetKey(strAuxSubKey(1))
                Else
                    If strAuxSubKey.Count >= 4 AndAlso strAuxSubKey(2).Trim.ToUpper = "#ELSE;" Then
                        strReturn = strReturn & fnGetKey(strAuxSubKey(3))
                    End If
                End If

            ElseIf p_strKey.Trim.ToUpper.StartsWith("#IFNOT_LASTFIELD;") Then
                strAuxSubKey = p_strKey.Split(">").ToList

                If objSelectedField IsNot objTableInfo.Columns.Last Then
                    strReturn = strReturn & fnGetKey(strAuxSubKey(1))
                Else
                    If strAuxSubKey.Count >= 4 AndAlso strAuxSubKey(2).Trim.ToUpper = "#ELSE;" Then
                        strReturn = strReturn & fnGetKey(strAuxSubKey(3))
                    End If
                End If

            ElseIf p_strKey.Trim.ToUpper.StartsWith("#IFNOT_FIRSTFIELD;") Then
                strAuxSubKey = p_strKey.Split(">").ToList

                If objSelectedField IsNot objTableInfo.Columns.First Then
                    strReturn = strReturn & fnGetKey(strAuxSubKey(1))
                Else
                    If strAuxSubKey.Count >= 4 AndAlso strAuxSubKey(2).Trim.ToUpper = "#ELSE;" Then
                        strReturn = strReturn & fnGetKey(strAuxSubKey(3))
                    End If
                End If





            ElseIf p_strKey.Trim.ToUpper.StartsWith("#FOREACH_INDEX;") Then

                strAuxSubKey = p_strKey.Split(">").ToList

                For Each Index As clsIndexInfo In objTableInfo.Indexes
                    objSelectedIndex = Index
                    strReturn = strReturn & fnGetKey(strAuxSubKey(1))
                Next

                objSelectedIndex = Nothing

            ElseIf p_strKey.Trim.ToUpper.StartsWith("#IF_LASTINDEX;") Then
                strAuxSubKey = p_strKey.Split(">").ToList

                If objSelectedIndex Is objTableInfo.Indexes.Last Then
                    strReturn = strReturn & fnGetKey(strAuxSubKey(1))
                Else
                    If strAuxSubKey.Count >= 4 AndAlso strAuxSubKey(2).Trim.ToUpper = "#ELSE;" Then
                        strReturn = strReturn & fnGetKey(strAuxSubKey(3))
                    End If
                End If

            ElseIf p_strKey.Trim.ToUpper.StartsWith("#IF_FIRSTINDEX;") Then
                strAuxSubKey = p_strKey.Split(">").ToList

                If objSelectedIndex Is objTableInfo.Indexes.First Then
                    strReturn = strReturn & fnGetKey(strAuxSubKey(1))
                Else
                    If strAuxSubKey.Count >= 4 AndAlso strAuxSubKey(2).Trim.ToUpper = "#ELSE;" Then
                        strReturn = strReturn & fnGetKey(strAuxSubKey(3))
                    End If
                End If

            ElseIf p_strKey.Trim.ToUpper.StartsWith("#IFNOT_LASTINDEX;") Then
                strAuxSubKey = p_strKey.Split(">").ToList

                If objSelectedIndex IsNot objTableInfo.Indexes.Last Then
                    strReturn = strReturn & fnGetKey(strAuxSubKey(1))
                Else
                    If strAuxSubKey.Count >= 4 AndAlso strAuxSubKey(2).Trim.ToUpper = "#ELSE;" Then
                        strReturn = strReturn & fnGetKey(strAuxSubKey(3))
                    End If
                End If

            ElseIf p_strKey.Trim.ToUpper.StartsWith("#IFNOT_FIRSTINDEX;") Then
                strAuxSubKey = p_strKey.Split(">").ToList

                If objSelectedIndex IsNot objTableInfo.Indexes.First Then
                    strReturn = strReturn & fnGetKey(strAuxSubKey(1))
                Else
                    If strAuxSubKey.Count >= 4 AndAlso strAuxSubKey(2).Trim.ToUpper = "#ELSE;" Then
                        strReturn = strReturn & fnGetKey(strAuxSubKey(3))
                    End If
                End If



            ElseIf p_strKey.Trim.ToUpper.StartsWith("#FIELD_TOSTRING;") Then

                If objSelectedField IsNot Nothing Then

                    strAuxSubKey = p_strKey.Split(">").ToList

                    Select Case objSelectedField.Type
                        Case enm_ColumnType.Text
                            strReturn = ""
                        Case enm_ColumnType.Xml
                            strReturn = fnGetKey("#TOSTRING_XML_PREFIX;")
                        Case enm_ColumnType.Number
                            strReturn = fnGetKey("#TOSTRING_NUMBER_PREFIX;")
                        Case enm_ColumnType.Int
                            strReturn = fnGetKey("#TOSTRING_INTEGER_PREFIX;")
                        Case enm_ColumnType.File
                            strReturn = ""
                        Case enm_ColumnType.DateTime
                            strReturn = fnGetKey("#TOSTRING_DATE_PREFIX;")
                        Case enm_ColumnType.Bool
                            strReturn = fnGetKey("#TOSTRING_BOOLEAN_PREFIX;")
                        Case Else
                            strReturn = ""
                    End Select

                    strReturn = strReturn & fnGetKey(strAuxSubKey(1))

                    Select Case objSelectedField.Type
                        Case enm_ColumnType.Text
                            strReturn = strReturn & ""
                        Case enm_ColumnType.Xml
                            strReturn = strReturn & fnGetKey("#TOSTRING_XML_SUFIX;")
                        Case enm_ColumnType.Number
                            strReturn = strReturn & fnGetKey("#TOSTRING_NUMBER_SUFIX;")
                        Case enm_ColumnType.Int
                            strReturn = strReturn & fnGetKey("#TOSTRING_INTEGER_SUFIX;")
                        Case enm_ColumnType.File
                            strReturn = strReturn & ""
                        Case enm_ColumnType.DateTime
                            strReturn = strReturn & fnGetKey("#TOSTRING_DATE_SUFIX;")
                        Case enm_ColumnType.Bool
                            strReturn = strReturn & fnGetKey("#TOSTRING_BOOLEAN_SUFIX;")
                        Case Else
                            strReturn = strReturn & ""
                    End Select

                End If


            ElseIf p_strKey.Trim.ToUpper.StartsWith("#FIELD_FROMSTRING;") Then

                If objSelectedField IsNot Nothing Then

                    strAuxSubKey = p_strKey.Split(">").ToList

                    Select Case objSelectedField.Type
                        Case enm_ColumnType.Text
                            strReturn = ""
                        Case enm_ColumnType.Xml
                            strReturn = fnGetKey("#FROMSTRING_XML_PREFIX;")
                        Case enm_ColumnType.Number
                            strReturn = fnGetKey("#FROMSTRING_NUMBER_PREFIX;")
                        Case enm_ColumnType.Int
                            strReturn = fnGetKey("#FROMSTRING_INTEGER_PREFIX;")
                        Case enm_ColumnType.File
                            strReturn = strReturn & ""
                        Case enm_ColumnType.DateTime
                            strReturn = fnGetKey("#FROMSTRING_DATE_PREFIX;")
                        Case enm_ColumnType.Bool
                            strReturn = fnGetKey("#FROMSTRING_BOOLEAN_PREFIX;")
                        Case Else
                            strReturn = fnGetKey("")
                    End Select

                    strReturn = strReturn & fnGetKey(strAuxSubKey(1))

                    Select Case objSelectedField.Type
                        Case enm_ColumnType.Text
                            strReturn = strReturn & ""
                        Case enm_ColumnType.Xml
                            strReturn = strReturn & fnGetKey("#FROMSTRING_XML_SUFIX;")
                        Case enm_ColumnType.Number
                            strReturn = strReturn & fnGetKey("#FROMSTRING_NUMBER_SUFIX;")
                        Case enm_ColumnType.Int
                            strReturn = strReturn & fnGetKey("#FROMSTRING_INTEGER_SUFIX;")
                        Case enm_ColumnType.File
                            strReturn = strReturn & ""
                        Case enm_ColumnType.DateTime
                            strReturn = strReturn & fnGetKey("#FROMSTRING_DATE_SUFIX;")
                        Case enm_ColumnType.Bool
                            strReturn = strReturn & fnGetKey("#FROMSTRING_BOOLEAN_SUFIX;")
                        Case Else
                            strReturn = strReturn & ""
                    End Select

                End If

            ElseIf p_strKey.Trim.ToUpper.StartsWith("#NORMALIZE;") Then

                strAuxSubKey = p_strKey.Split(">").ToList
                strReturn = fnGetKey(strAuxSubKey(1))
                If strReturn.Trim.Count > 1 Then
                    strReturn = strReturn.Substring(0, 1).ToUpper & strReturn.Substring(1).ToLower
                End If

            Else

                Select Case p_strKey.Trim.ToUpper
                    Case "#NEWLINE;"
                        strReturn = vbNewLine
                    Case "#SPACE;"
                        strReturn = " "
                    Case "#TAB;"
                        strReturn = vbTab
                    Case "#COMMA;"
                        strReturn = ","
                    Case "#PERIOD;"
                        strReturn = "."
                    Case "#SEMICOLON;"
                        strReturn = ";"
                    Case "#BASE_NAME;"
                        strReturn = strBaseName
                    Case "#TABLE_NAME;"
                        strReturn = objTableInfo.Name
                    Case "#FIELD_INDEX;"
                        If objSelectedField IsNot Nothing Then
                            strReturn = objSelectedField.Index
                        End If
                    Case "#FIELD_NAME;"
                        If objSelectedField IsNot Nothing Then
                            strReturn = objSelectedField.Name
                        End If
                    Case "#FIELD_TYPE;"
                        If objSelectedField IsNot Nothing Then
                            Select Case objSelectedField.Type
                                Case enm_ColumnType.Text
                                    strReturn = fnGetKey("#STRINGTYPE;")
                                Case enm_ColumnType.Xml
                                    strReturn = fnGetKey("#XMLTYPE;")
                                Case enm_ColumnType.Number
                                    strReturn = fnGetKey("#NUMBERTYPE;")
                                Case enm_ColumnType.Int
                                    strReturn = fnGetKey("#INTEGERTYPE;")
                                Case enm_ColumnType.File
                                    strReturn = fnGetKey("#FILETYPE;")
                                Case enm_ColumnType.DateTime
                                    strReturn = fnGetKey("#DATETYPE;")
                                Case enm_ColumnType.Bool
                                    strReturn = fnGetKey("#BOOLEANTYPE;")
                                Case Else
                                    strReturn = fnGetKey("#OBJECTTYPE;")
                            End Select
                        End If
                    Case "#FIELD_PREFIX;"
                        If objSelectedField IsNot Nothing Then
                            Select Case objSelectedField.Type
                                Case enm_ColumnType.Text
                                    strReturn = fnGetKey("#STRINGPREFIX;")
                                Case enm_ColumnType.Xml
                                    strReturn = fnGetKey("#XMLPREFIX;")
                                Case enm_ColumnType.Number
                                    strReturn = fnGetKey("#NUMBERPREFIX;")
                                Case enm_ColumnType.Int
                                    strReturn = fnGetKey("#INTEGERPREFIX;")
                                Case enm_ColumnType.File
                                    strReturn = fnGetKey("#FILEPREFIX;")
                                Case enm_ColumnType.DateTime
                                    strReturn = fnGetKey("#DATEPREFIX;")
                                Case enm_ColumnType.Bool
                                    strReturn = fnGetKey("#BOOLEANPREFIX;")
                                Case Else
                                    strReturn = fnGetKey("#OBJECTPREFIX;")
                            End Select
                        End If

                    Case "#FIELD_INITIALIZER;"
                        If objSelectedField IsNot Nothing Then
                            Select Case objSelectedField.Type
                                Case enm_ColumnType.Text
                                    strReturn = fnGetKey("#STRINGINITIALIZER;")
                                Case enm_ColumnType.Xml
                                    strReturn = fnGetKey("#XMLINITIALIZER;")
                                Case enm_ColumnType.Number
                                    strReturn = fnGetKey("#NUMBERINITIALIZER;")
                                Case enm_ColumnType.Int
                                    strReturn = fnGetKey("#INTEGERINITIALIZER;")
                                Case enm_ColumnType.File
                                    strReturn = fnGetKey("#FILEINITIALIZER;")
                                Case enm_ColumnType.DateTime
                                    strReturn = fnGetKey("#DATEINITIALIZER;")
                                Case enm_ColumnType.Bool
                                    strReturn = fnGetKey("#BOOLEANINITIALIZER;")
                                Case Else
                                    strReturn = fnGetKey("#OBJECTINITIALIZER;")
                            End Select
                        End If

                End Select

            End If

            Return strReturn

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Overridable Function fnGetKey(p_strkey As String) As String Implements IPatterns.fnGetKey

        Dim strReturn As String
        Dim objAuxPaternItem As clsPaternItem
        Dim objAuxPaternGroup As clsPaternGroup

        Try

            strReturn = fnGetSpecialKeys(p_strkey)

            If strReturn = "" Then

                objAuxPaternItem = objPaternItens.Find(Function(Item As clsPaternItem) Item.Key.Trim.ToUpper = p_strkey.Trim.ToUpper)
                If objAuxPaternItem IsNot Nothing Then
                    strReturn = objAuxPaternItem.Value
                Else
                    objAuxPaternGroup = objPaternGroups.Find(Function(Item As clsPaternGroup) Item.Key.Trim.ToUpper = p_strkey.Trim.ToUpper)
                    If objAuxPaternGroup IsNot Nothing Then
                        For Each SubItem As String In objAuxPaternGroup.Values
                            strReturn = strReturn & fnGetKey(SubItem)
                        Next
                    Else
                        strReturn = p_strkey
                    End If
                End If
            End If

            Return strReturn

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function fnMountSingle(p_strSequence As List(Of String)) As String Implements IPatterns.fnMountSingle

        Dim strReturn As String

        Try
            strReturn = ""

            For Each Item As String In p_strSequence
                strReturn = strReturn & fnGetKey(Item)
            Next

            Return strReturn

        Catch ex As Exception
            Throw
        End Try
    End Function
#End Region

#Region "Properties"

    Public Property Name() As String Implements IPatterns.Name
        Get
            Return strName
        End Get
        Set(ByVal value As String)
            strName = value
        End Set
    End Property

    Public Property PaternItens As List(Of clsPaternItem) Implements IPatterns.PaternItens
        Get
            Return objPaternItens
        End Get
        Set(value As List(Of clsPaternItem))
            objPaternItens = value
        End Set
    End Property

    Public Property PaternFiles As List(Of clsPaternFile) Implements IPatterns.PaternFiles
        Get
            Return objPaternFiles
        End Get
        Set(value As List(Of clsPaternFile))
            objPaternFiles = value
        End Set
    End Property

    Public Property PaternGroups As List(Of clsPaternGroup) Implements IPatterns.PaternGroups
        Get
            Return objPaternGroups
        End Get
        Set(value As List(Of clsPaternGroup))
            objPaternGroups = value
        End Set
    End Property

    Public Property TargetLanguage As enm_OutputLanguage Implements IPatterns.TargetLanguage
        Get
            Return enmTargetLanguage
        End Get
        Set(value As enm_OutputLanguage)
            enmTargetLanguage = value
        End Set
    End Property

    Public Property TableInfo As clsTableInfo Implements IPatterns.TableInfo
        Get
            Return objTableInfo
        End Get
        Set(value As clsTableInfo)
            objTableInfo = value
        End Set
    End Property

    Public Property BaseName() As String Implements IPatterns.BaseName
        Get
            Return strBaseName
        End Get
        Set(ByVal value As String)
            strBaseName = value
        End Set
    End Property
#End Region

End Class

Public Class clsPaternItem
#Region "Declarations"
    Private strKey As String
    Private strValue As String
#End Region

#Region "Constructor"
    Public Sub New()
        Try
            strKey = ""
            strValue = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub New(p_strKey As String, p_strValue As String)
        Try
            strKey = p_strKey
            strValue = p_strValue
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub New(p_xmlInput As XDocument)
        Try
            Call fromXml(p_xmlInput)
        Catch ex As Exception
            Throw
        End Try
    End Sub
#End Region

#Region "Functions"
    Public Function toXml() As XDocument

        Dim xmlReturn As XDocument

        Try


            xmlReturn = New XDocument(<PaternItem>
                                          <Key><%= strKey %></Key>
                                          <Value><%= strValue %></Value>
                                      </PaternItem>)

            Return xmlReturn

        Catch ex As Exception
            Throw
        End Try
    End Function

    Protected Sub fromXml(p_xmlInput As XDocument)
        Try
            strKey = p_xmlInput.<PaternItem>.<Folder>.Value
            strValue = p_xmlInput.<PaternItem>.<Value>.Value
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function Clone() As clsPaternItem
        Try
            Return New clsPaternItem(toXml)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function serialize() As String
        Try
            Return Json.JsonConvert.SerializeObject(Me, Json.Formatting.Indented)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Shared Function serialize(p_objInput As clsPaternItem) As String
        Try
            Return Json.JsonConvert.SerializeObject(p_objInput, Json.Formatting.Indented)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Shared Function deserialize(p_strJson As String) As clsPaternItem
        Try
            Return Json.JsonConvert.DeserializeObject(Of clsPaternItem)(p_strJson)
        Catch ex As Exception
            Throw
        End Try
    End Function
#End Region

#Region "Properties"
    <JsonProperty("KEY")>
    Public Property Key() As String
        Get
            Return strKey
        End Get
        Set(ByVal value As String)
            strKey = value
        End Set
    End Property

    <JsonProperty("VALUE")>
    Public Property Value() As String
        Get
            Return strValue
        End Get
        Set(ByVal value As String)
            strValue = value
        End Set
    End Property
#End Region
End Class

Public Class clsPaternGroup
#Region "Declarations"
    Private strKey As String
    Private strValues As List(Of String)
#End Region

#Region "Constructor"
    Public Sub New()
        Try
            strKey = ""
            strValues = New List(Of String)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub New(p_strKey As String, p_strValues As List(Of String))
        Try
            strKey = p_strKey
            strValues = p_strValues
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub New(p_xmlInput As XDocument)
        Try
            Call fromXml(p_xmlInput)
        Catch ex As Exception
            Throw
        End Try
    End Sub
#End Region

#Region "Functions"

    Public Function toXml() As XDocument

        Dim xmlReturn As XDocument

        Try


            xmlReturn = New XDocument(<PaternGroup>
                                          <Key><%= strKey %></Key>
                                      </PaternGroup>)

            xmlReturn.<PaternGroup>.First.Add(<Values></Values>)
            For Each Item As String In strValues
                xmlReturn.<PaternGroup>.<Values>.First.Add(<Item><%= Item %></Item>)
            Next

            Return xmlReturn

        Catch ex As Exception
            Throw
        End Try
    End Function

    Protected Sub fromXml(p_xmlInput As XDocument)

        Try
            strKey = p_xmlInput.<PaternGroup>.<Key>.Value

            strValues = New List(Of String)

            For Each Item As XElement In p_xmlInput.<PaternGroup>.<Values>.Elements
                strValues.Add(Item.Value)
            Next

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function Clone() As clsPaternGroup
        Try
            Return New clsPaternGroup(toXml)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function serialize() As String
        Try
            Return Json.JsonConvert.SerializeObject(Me, Json.Formatting.Indented)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Shared Function serialize(p_objInput As clsPaternGroup) As String
        Try
            Return Json.JsonConvert.SerializeObject(p_objInput, Json.Formatting.Indented)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Shared Function deserialize(p_strJson As String) As clsPaternGroup
        Try
            Return Json.JsonConvert.DeserializeObject(Of clsPaternGroup)(p_strJson)
        Catch ex As Exception
            Throw
        End Try
    End Function
#End Region

#Region "Properties"
    <JsonProperty("KEY")>
    Public Property Key() As String
        Get
            Return strKey
        End Get
        Set(ByVal value As String)
            strKey = value
        End Set
    End Property

    <JsonProperty("VALUES")>
    Public Property Values() As List(Of String)
        Get
            Return strValues
        End Get
        Set(ByVal value As List(Of String))
            strValues = value
        End Set
    End Property
#End Region
End Class

Public Class clsPaternFile
#Region "Declarations"
    Private strNameSequence As List(Of String)
    Private strFolder As String
    Private strComposition As List(Of String)
#End Region

#Region "Constructor"
    Public Sub New(p_strFolder As String, p_strNameSequence As List(Of String), p_strValues As List(Of String))
        Try
            strNameSequence = p_strNameSequence
            strFolder = p_strFolder
            strComposition = p_strValues
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub New()
        Try
            strNameSequence = New List(Of String)
            strFolder = ""
            strComposition = New List(Of String)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub New(p_xlmInput As XDocument)
        Try
            fromXml(p_xlmInput)
        Catch ex As Exception
            Throw
        End Try
    End Sub
#End Region

#Region "Functions"
    Public Function toXml() As XDocument

        Dim xmlReturn As XDocument

        Try


            xmlReturn = New XDocument(<PaternFile>
                                          <Folder><%= strFolder %></Folder>
                                      </PaternFile>)

            xmlReturn.<PaternFile>.First.Add(<NameSequence></NameSequence>)
            For Each Item As String In strNameSequence
                xmlReturn.<PaternFile>.<NameSequence>.First.Add(<Item><%= Item %></Item>)
            Next

            xmlReturn.<PaternFile>.First.Add(<Composition></Composition>)
            For Each Item As String In strComposition
                xmlReturn.<PaternFile>.<Composition>.First.Add(<Item><%= Item %></Item>)
            Next

            Return xmlReturn

        Catch ex As Exception
            Throw
        End Try
    End Function

    Protected Sub fromXml(p_xmlInput As XDocument)

        Try
            strFolder = p_xmlInput.<PaternFile>.<Folder>.Value

            strNameSequence = New List(Of String)
            strComposition = New List(Of String)

            For Each Item As XElement In p_xmlInput.<PaternFile>.<NameSequence>.Elements
                strNameSequence.Add(Item.Value)
            Next

            For Each Item As XElement In p_xmlInput.<PaternFile>.<Composition>.Elements
                strComposition.Add(Item.Value)
            Next

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function Clone() As clsPaternFile
        Try
            Return New clsPaternFile(toXml)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function serialize() As String
        Try
            Return Json.JsonConvert.SerializeObject(Me, Json.Formatting.Indented)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Shared Function serialize(p_objInput As clsPaternFile) As String
        Try
            Return Json.JsonConvert.SerializeObject(p_objInput, Json.Formatting.Indented)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Shared Function deserialize(p_strJson As String) As clsPaternFile
        Try
            Return Json.JsonConvert.DeserializeObject(Of clsPaternFile)(p_strJson)
        Catch ex As Exception
            Throw
        End Try
    End Function
#End Region

#Region "Properties"
    <JsonProperty("NAMESEQUENCE")>
    Public Property NameSequence As List(Of String)
        Get
            Return strNameSequence
        End Get
        Set(value As List(Of String))
            strNameSequence = value
        End Set
    End Property

    <JsonProperty("FOLDER")>
    Public Property Folder As String
        Get
            Return strFolder
        End Get
        Set(value As String)
            strFolder = value
        End Set
    End Property

    <JsonProperty("COMPOSITION")>
    Public Property Composition As List(Of String)
        Get
            Return strComposition
        End Get
        Set(value As List(Of String))
            strComposition = value
        End Set
    End Property
#End Region
End Class

Public Class clsGeneratedFile

#Region "Declarations"
    Private strFileFolder As String
    Private strFileName As String
    Private strLines As List(Of String)
#End Region

#Region "Constructor"
    Public Sub New(p_strFolder As String, p_strName As String)
        Try
            strFileFolder = p_strFolder
            strFileName = p_strName
            strLines = New List(Of String)
        Catch ex As Exception
            Throw
        End Try
    End Sub
#End Region

#Region "Properties"
    Public Property Lines() As List(Of String)
        Get
            Return strLines
        End Get
        Set(ByVal value As List(Of String))
            strLines = value
        End Set
    End Property

    Public Property FileFolder() As String
        Get
            Return strFileFolder
        End Get
        Set(ByVal value As String)
            strFileFolder = value
        End Set
    End Property

    Public Property FileName() As String
        Get
            Return strFileName
        End Get
        Set(ByVal value As String)
            strFileName = value
        End Set
    End Property


#End Region

End Class
