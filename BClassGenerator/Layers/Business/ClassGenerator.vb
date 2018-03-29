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

Imports BDataBase

Public Enum enm_OutputLanguage
    VbNet
    CSharp
    Kotlin
    Phyton
End Enum

Public Enum enm_CommentType
    ClassComment
    FunctionComment
    SubroutineComment
    PropertyComment
End Enum

Public Enum enm_VariableType
    StringType
    IntegerType
    DoubleType
    BooleanType
    XmlType
    ObjectType
End Enum

Public Enum enm_MethodType
    FunctionType
    SubroutineType
    ConstructorType
End Enum

Public Enum enm_ClassType
    ClassType
    RepositoryType
    BusinessType
    BasicType
    InterfaceType
    FactoryType
    AbstractType
End Enum

Public Enum enm_AccessType
    PublicAccess
    PrivateAccess
    ProtectedAccess
    FriendAccess
    DefaultAccess
End Enum

Public Enum enm_ModifierType
    DefaultModifier
    SharedModifier
    ReadOnlyModifier
    OverrideModifier
    OverloadModifier
    AbstractModifier
    FinalModifier
End Enum

Public Class ClassGenerator

#Region "Declarations"
    Private objPatern As IPatterns
    Private ClassGeneratorCore As IGenerator
    Private enmSourceDataBaseType As DataBase.enmDataBaseType
    Private objDataBase As IDataBase
#End Region

#Region "Constructor"
    Public Sub New(p_strServer As String, p_strDataBase As String, p_strUser As String, p_strPassword As String, p_enmSourceDataBaseType As DataBase.enmDataBaseType)
        Try

            enmSourceDataBaseType = p_enmSourceDataBaseType
            objPatern = Nothing
            objDataBase = DataBase.fnOpenConnection(p_strServer, p_strDataBase, p_strUser, p_strPassword, p_enmSourceDataBaseType)

        Catch ex As Exception
            Throw
        End Try
    End Sub
#End Region

#Region "Functions and Subroutines"
    Public Function fnGenerateByTable(p_strTable As String, p_objPatern As IPatterns) As List(Of clsGeneratedFile)

        Dim objReturn As List(Of clsGeneratedFile)
        Dim objTableInfo As clsTableInfo

        Try

            objReturn = New List(Of clsGeneratedFile)

            'Get Table Info
            objTableInfo = objDataBase.fnGetTableInfo(p_strTable)

            objPatern = p_objPatern
            objPatern.TableInfo = objTableInfo

            'Generate files
            '----------------------------------------------------------
            For Each File As clsPaternFile In objPatern.PaternFiles

                objReturn.Add(New clsGeneratedFile(File.Folder, objPatern.fnMountSingle(File.NameSequence)))
                objReturn.Last.Lines.Add(objPatern.fnMountSingle(File.Composition))

            Next
            '----------------------------------------------------------


            Return objReturn

        Catch ex As Exception
            Throw
        End Try
    End Function
#End Region

#Region "Properties"

#End Region

End Class
