Imports System.IO
Imports System.Text.RegularExpressions

Class MainWindow
    Public Event Save()
    Private gSpells As Dictionary(Of String, Spell)

    Private Function GetTimeString(actionType As Time) As String
        Select Case actionType
            Case Time.Action
                Return "1 action"
            Case Time.BonusAction
                Return "1 bonus action"
            Case Time.Reaction
                Return "1 reaction"
            Case Time.Instantaneous
                Return "Instantaneous"
            Case Time.Permanent
                Return "Permament"
            Case Else
                Return GetTimeString(1, actionType)
        End Select
    End Function

    Private Function GetTimeString(number As Integer, actionType As Time)
        Select Case actionType
            Case Time.Round
                If number > 1 Then Return number & " rounds" Else Return "1 round"
            Case Time.Minute
                If number > 1 Then Return number & " minutes" Else Return "1 minute"
            Case Time.Hour
                If number >= 24 Then Return "24+ hours" Else If number > 1 Then Return number & " hours" Else Return "1 hour"
            Case Time.Special
                Return ""
        End Select
    End Function

    ' Begin SCHOOLS OF MAGIC
    Enum School
        Abjuration
        Conjuration
        Divination
        Enchantment
        Evocation
        Illusion
        Necromancy
        Transmutation
    End Enum

    Dim gStringSchools As Dictionary(Of String, School) = New Dictionary(Of String, School) From {
            {"Abjuration", School.Abjuration},
            {"Conjuration", School.Conjuration},
            {"Divination", School.Divination},
            {"Enchantment", School.Enchantment},
            {"Evocation", School.Evocation},
            {"Illusion", School.Illusion},
            {"Necromancy", School.Necromancy},
            {"Transmutation", School.Transmutation}
       }

    ' Begin TYPES OF SPELLS

    Enum Category
        Buff
        Control
        Creation
        Damage
        Debuff
        Detection
        Exchange
        Healing
        Negation
        Social
        Special
        Summoning
        Teleportation
        Utility
    End Enum

    Dim gStringCategories As Dictionary(Of String, Category) = New Dictionary(Of String, Category) From {
            {"Buff", Category.Buff},
            {"Control", Category.Control},
            {"Creation", Category.Creation},
            {"Damage", Category.Damage},
            {"Debuff", Category.Debuff},
            {"Detection", Category.Detection},
            {"Exchange", Category.Exchange},
            {"Healing", Category.Healing},
            {"Negation", Category.Negation},
            {"Social", Category.Social},
            {"Special", Category.Special},
            {"Summoning", Category.Summoning},
            {"Teleportation", Category.Teleportation},
            {"Utility", Category.Utility}
        }

    ' Begin ACTION ECONOMY
    Enum Time
        Action
        BonusAction
        Reaction
        Round
        Minute
        Hour
        Special
        Instantaneous
        Permanent
    End Enum

    ' Begin DAMAGE
    Enum Damage
        None
        Acid
        Bludgeoning
        Cold
        Fire
        Force
        Lightning
        Necrotic
        Piercing
        Poison
        Psychic
        Radiant
        Slashing
        Thunder
        Physical
        Elemental
        Outerplanar
    End Enum

    Dim gStringDamages As Dictionary(Of String, Category) = New Dictionary(Of String, Category) From {
        {"—", Damage.None},
        {"Acid", Damage.Acid},
        {"Bludgeoning", Damage.Bludgeoning},
        {"Cold", Damage.Cold},
        {"Fire", Damage.Fire},
        {"Force", Damage.Force},
        {"Lightning", Damage.Lightning},
        {"Necrotic", Damage.Necrotic},
        {"Piercing", Damage.Piercing},
        {"Poison", Damage.Poison},
        {"Psychic", Damage.Psychic},
        {"Radiant", Damage.Radiant},
        {"Slashing", Damage.Slashing},
        {"Thunder", Damage.Thunder},
        {"Physical", Damage.Physical},
        {"Elemental", Damage.Elemental},
        {"Outerplanar", Damage.Outerplanar}
    }

    ' Begin ATTACK TYPES
    Enum Attack
        Melee
        Ranged
        Guaranteed
        STR
        DEX
        CON
        INT
        WIS
        CHA
    End Enum

    Private Function GetAttackTypeString(type As Attack) As String
        Select Case type
            Case Attack.Melee
                Return "Melee"
            Case Attack.Ranged
                Return "Ranged"
            Case Attack.Guaranteed
                Return "Guaranteed"
            Case Attack.STR
                Return "STR Save"
            Case Attack.DEX
                Return "DEX Save"
            Case Attack.CON
                Return "CON Save"
            Case Attack.INT
                Return "INT Save"
            Case Attack.WIS
                Return "WIS Save"
            Case Attack.CHA
                Return "CHA Save"
        End Select

    End Function

    Private Function GetDiceString(type As Integer)
        Return "d" & type
    End Function

    ' Begin TARGETS, RANGES, AREAS

    Enum Target
        Self
        Creature
        VisibleCreature
        HearingCreature
        Humanoid
        VisibleHumanoid
        HearingHumanoid
        Item
        VisibleItem
        Point
        VisiblePoint
    End Enum

    Function GetTargetString(type As Target)
        Select Case type
            Case Target.Self
                Return "Self"
            Case Target.Creature
                Return "Creature"
            Case Target.VisibleCreature
                Return "Creature you can see"
            Case Target.HearingCreature
                Return "Creature that can hear you"
            Case Target.Humanoid
                Return "Humanoid"
            Case Target.VisibleHumanoid
                Return "Humanoid you can see"
            Case Target.HearingHumanoid
                Return "Humanoid that can hear you"
            Case Target.Item
                Return "Object"
            Case Target.VisibleItem
                Return "Object you can see"
            Case Target.Point
                Return "Point"
            Case Target.VisiblePoint
                Return "Point you can see"
        End Select
    End Function

    Enum Range
        Self
        SelfArea
        Touch
        Five
        Ten
        Twenty
        Thirty
        Sixty
        Ninety
        OneTwenty
        OneFifty
        ThreeHundred
        Special
    End Enum

    Function GetRangeString(distance As Range)
        Select Case distance
            Case Range.Self
                Return "Self"
            Case Range.SelfArea
                Return "Self (Area)"
            Case Range.Touch
                Return "Touch"
            Case Range.Five
                Return "5 feet"
            Case Range.Ten
                Return "10 feet"
            Case Range.Twenty
                Return "20 feet"
            Case Range.Thirty
                Return "30 feet"
            Case Range.Sixty
                Return "60 feet"
            Case Range.Ninety
                Return "90 feet"
            Case Range.OneTwenty
                Return "120 feet"
            Case Range.OneFifty
                Return "150 feet"
            Case Range.ThreeHundred
                Return "300 feet"
            Case Range.Special
                Return "Special"
        End Select
    End Function

    Enum Shape
        None
        Sphere
        Cube
        Cone
        Cylinder
        Line
    End Enum

    Function GetShapeString(type As Shape)
        Select Case type
            Case Shape.None
                Return "—"
            Case Shape.Sphere
                Return "Sphere"
            Case Shape.Cube
                Return "Cube"
            Case Shape.Cone
                Return "Cone"
            Case Shape.Cylinder
                Return "Cylinder"
            Case Shape.Line
                Return "Line"
        End Select
    End Function

    Private Sub SetSizesByShape(type As Shape)
        cboSize.Items.Clear()
        Dim sizes() As String = {}
        Select Case type
            Case Shape.None
                sizes = {
                    "—"
                }
            Case Shape.Sphere
                sizes = {
                    "5-foot-diameter",
                    "5-foot-radius",
                    "10-foot-radius",
                    "15-foot-radius",
                    "20-foot-radius",
                    "30-foot-radius",
                    "40-foot-radius",
                    "60-foot-radius"
                }
            Case Shape.Cube
                sizes = {
                    "5-foot",
                    "10-foot",
                    "15-foot",
                    "20-foot",
                    "30-foot",
                    "40-foot",
                    "100-foot"
                }
            Case Shape.Cone
                sizes = {
                    "15-foot",
                    "30-foot",
                    "60-foot"
                }
            Case Shape.Cylinder
                sizes = {
                    "5-foot-radius",
                    "10-foot-radius",
                    "20-foot-radius",
                    "40-foot-radius",
                    "50-foot-radius",
                    "60-foot-radius"
                }
            Case Shape.Line
                sizes = {
                    "30 feet long and 5 feet wide",
                    "60 feet long and 10 feet wide",
                    "60 feet long and 5 feet wide",
                    "100 feet long and 5 feet wide"
                }
            Case Else
                Return
        End Select
        For Each strOption As String In sizes
            cboSize.Items.Add(strOption)
        Next
    End Sub

    ' Begin CLASSES
    Dim gClasses() As String = {
            "Artificer",
            "Barbarian",
            "Bard",
            "Cleric",
            "Druid",
            "Paladin",
            "Ranger",
            "Sorcerer",
            "Warlock",
            "Wizard"
        }

    ' Begin CONDITIONS
    Dim gConditions() As String = {
        "Advantage",
        "Blinded",
        "Bonus",
        "Charmed",
        "Deafened",
        "Disadvantage",
        "Exhaustion",
        "Frightened",
        "Grappled",
        "Immune",
        "Incapacitated",
        "Invisible",
        "Paralyzed",
        "Penalty",
        "Petrified",
        "Poisoned",
        "Prone",
        "Resistant",
        "Restrained",
        "Stunned",
        "Unconscious",
        "Vulnerable"
    }

    Private Function StringifyCondition(condition As String) As String
        Select Case condition.Trim()
            Case "Advantage", "Disadvantage"
                Dim result As New System.Text.StringBuilder
                result.Append("has " & LCase(condition) & " on ")
                If chkTheNext.IsChecked Then
                    result.Append("its next ")
                ElseIf chkSingle.IsChecked Then
                    result.Append("one ")
                End If
                If cboAttackSave.Items.Contains(lstDisAdvantageOptions.SelectedItem) Then result.Append(StringifySave(lstDisAdvantageOptions.SelectedItem)) Else result.Append(lstDisAdvantageOptions.SelectedItem)
                Return result.ToString()
            Case "Bonus"
                Dim result As New System.Text.StringBuilder
                result.Append("gains a bonus of " & txtBonusPenaltyDieAmount.Text.Trim() & cboBonusPenaltyDieType.SelectedItem & " to add to the result of ")
                If chkTheNext.IsChecked Then
                    result.Append("its next ")
                ElseIf chkSingle.IsChecked Then
                    result.Append("one ")
                End If
                If cboAttackSave.Items.Contains(lstDisAdvantageOptions.SelectedItem) Then result.Append(StringifySave(lstDisAdvantageOptions.SelectedItem)) Else result.Append(lstDisAdvantageOptions.SelectedItem)
                Return result.ToString()
            Case "Penalty"
                Dim result As New System.Text.StringBuilder
                result.Append("suffers a penalty of " & txtBonusPenaltyDieAmount.Text.Trim() & cboBonusPenaltyDieType.SelectedItem & " to subtract from the result of ")
                If chkTheNext.IsChecked Then
                    result.Append("its next ")
                ElseIf chkSingle.IsChecked Then
                    result.Append("one ")
                End If
                If cboAttackSave.Items.Contains(lstDisAdvantageOptions.SelectedItem) Then result.Append(StringifySave(lstDisAdvantageOptions.SelectedItem)) Else result.Append(lstDisAdvantageOptions.SelectedItem)
                Return result.ToString()
            Case "Charmed"
                Return "is charmed by you"
            Case "Exhaustion"
                Return "gains a level of exhaustion"
            Case "Frightened"
                Return "is frightened of you"
            Case "Prone"
                Return "is knocked prone"
            Case "Unconscious"
                Return "falls unconscious"
            Case "Vulnerable", "Resistant", "Immune"
                Return "is " & LCase(condition) & " to " & LCase(cboDamage.SelectedItem) & "damage"
            Case Else
                Return "is " & LCase(condition)
        End Select
    End Function

    Private Function GetOrderString(order As Integer)
        Select Case order
            Case 0, 4, 5, 6, 7, 8, 9
                Return order & "th"
            Case 1
                Return order & "st"
            Case 2
                Return order & "nd"
            Case 3
                Return order & "rd"
            Case Else
                Return ""
        End Select
    End Function

    Private Sub SetCastingTimeSelection()
        cboCastingTime.Items.Clear()
        cboCastingTime.Items.Add(GetTimeString(Time.Action))
        cboCastingTime.Items.Add(GetTimeString(Time.BonusAction))
        cboCastingTime.Items.Add(GetTimeString(Time.Reaction))
        cboCastingTime.Items.Add(GetTimeString(Time.Round))
        cboCastingTime.Items.Add(GetTimeString(Time.Minute))
        cboCastingTime.Items.Add(GetTimeString(10, Time.Minute))
        cboCastingTime.Items.Add(GetTimeString(Time.Hour))
        cboCastingTime.SelectedItem = GetTimeString(Time.Action)
    End Sub

    Private Sub SetDurationSelection()
        cboDuration.Items.Clear()
        cboDuration.Items.Add(GetTimeString(Time.Instantaneous))
        cboDuration.Items.Add(GetTimeString(Time.Round))
        cboDuration.Items.Add(GetTimeString(Time.Minute))
        cboDuration.Items.Add(GetTimeString(10, Time.Minute))
        cboDuration.Items.Add(GetTimeString(Time.Hour))
        cboDuration.Items.Add(GetTimeString(8, Time.Hour))
        cboDuration.Items.Add(GetTimeString(24, Time.Hour))
        cboDuration.SelectedItem = "Instantaneous"
    End Sub

    Private Sub Init(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If gSpells Is Nothing Then gSpells = New Dictionary(Of String, Spell)
        ProcessSrdSpells()
        ProcessExtraSpells("spells_extended.txt")
        chkVerbal.IsChecked = True
        chkSomatic.IsChecked = True
        chkMaterial.IsChecked = True
        For Each stringOption As String In gStringSchools.Keys
            If Not cboSchool.Items.Contains(stringOption) Then cboSchool.Items.Add(stringOption)
        Next
        cboSchool.SelectedItem = "Evocation"
        For Each stringOption As String In gStringCategories.Keys
            If Not cboCategory.Items.Contains(stringOption) Then cboCategory.Items.Add(stringOption)
        Next
        cboCategory.SelectedItem = "Damage"
        For i As Integer = 0 To 9
            If Not cboLevel.Items.Contains(i) Then cboLevel.Items.Add(i)
        Next
        cboLevel.SelectedItem = 3
        For Each stringOption As String In gStringDamages.Keys
            If Not cboDamage.Items.Contains(stringOption) Then cboDamage.Items.Add(stringOption)
        Next
        SetCastingTimeSelection()
        cboDamage.SelectedItem = "Fire"
        If Not cboDamageDie.Items.Contains("—") Then cboDamageDie.Items.Add("—")
        For i As Integer = 2 To 6
            Dim dieString As String = GetDiceString(2 * i)
            If Not cboDamageDie.Items.Contains(dieString) Then cboDamageDie.Items.Add(dieString)
            If Not cboBonusPenaltyDieType.Items.Contains(dieString) Then cboBonusPenaltyDieType.Items.Add(dieString)
        Next
        cboDamageDie.SelectedItem = "d6"
        cboBonusPenaltyDieType.SelectedItem = "d4"
        chkUpcast.IsChecked = True
        For Each type As Attack In [Enum].GetValues(GetType(Attack))
            Dim strOption = GetAttackTypeString(type)
            If Not cboAttackSave.Items.Contains(strOption) Then cboAttackSave.Items.Add(strOption)
        Next
        cboAttackSave.SelectedItem = "DEX Save"
        chkHalfDamage.IsChecked = True
        ' TODO: Conditions that can be inflicted
        If Not cboInflicts.Items.Contains("—") Then cboInflicts.Items.Add("—")
        For Each condition As String In gConditions
            If Not cboInflicts.Items.Contains(condition) Then cboInflicts.Items.Add(condition)
        Next
        SetDurationSelection()
        For Each type As Target In [Enum].GetValues(GetType(Target))
            Dim strOption = GetTargetString(type)
            If Not cboTarget.Items.Contains(strOption) Then cboTarget.Items.Add(strOption)
        Next
        cboTarget.SelectedItem = "Point"
        For Each type As Range In [Enum].GetValues(GetType(Range))
            Dim strOption = GetRangeString(type)
            If Not cboRange.Items.Contains(strOption) Then cboRange.Items.Add(strOption)
        Next
        cboRange.SelectedItem = "150 feet"
        For Each type As Shape In [Enum].GetValues(GetType(Shape))
            Dim strOption = GetShapeString(type)
            If Not cboShape.Items.Contains(strOption) Then cboShape.Items.Add(strOption)
        Next
        cboShape.SelectedItem = "Sphere"
        SetSizesByShape(Shape.Sphere)
        cboSize.SelectedItem = "20 ft. radius"
        For Each dndClass As String In gClasses
            If Not lstClasses.Items.Contains(dndClass) Then lstClasses.Items.Add(dndClass)
        Next
        lstClasses.SelectedItems.Add("Sorcerer")
        lstClasses.SelectedItems.Add("Wizard")
    End Sub

    Private Sub SetColorBySchool(chosen As String)
        Dim schoolColor As SolidColorBrush
        Select Case chosen
            Case "Abjuration"
                schoolColor = New SolidColorBrush(Colors.CornflowerBlue)
            Case "Conjuration"
                schoolColor = New SolidColorBrush(Colors.DarkOrange)
            Case "Divination"
                schoolColor = New SolidColorBrush(Colors.LightSkyBlue)
            Case "Enchantment"
                schoolColor = New SolidColorBrush(Colors.HotPink)
            Case "Evocation"
                schoolColor = New SolidColorBrush(Colors.Crimson)
            Case "Illusion"
                schoolColor = New SolidColorBrush(Colors.Orchid)
            Case "Necromancy"
                schoolColor = New SolidColorBrush(Colors.YellowGreen)
            Case "Transmutation"
                schoolColor = New SolidColorBrush(Colors.Goldenrod)
            Case Else
                schoolColor = New SolidColorBrush(Colors.Black)
        End Select
        rectCardBack.Fill = schoolColor
        lblCardParts1.Foreground = schoolColor
        lblCardParts2.Foreground = schoolColor
        lblCardParts3.Foreground = schoolColor
        lblCardParts4.Foreground = schoolColor
    End Sub

    Private Function StringifySave(save As String)
        Select Case save
            Case "STR Save"
                Return "Strength saving throw"
            Case "DEX Save"
                Return "Dexterity saving throw"
            Case "CON Save"
                Return "Constitution saving throw"
            Case "INT Save"
                Return "Intelligence saving throw"
            Case "WIS Save"
                Return "Wisdom saving throw"
            Case "CHA Save"
                Return "Charisma saving throw"
        End Select
    End Function

    Private Function WriteAtHigherLevels() As String
        Dim level As Integer = CInt(cboLevel.SelectedItem)
        If level <> 0 And cboCategory.SelectedItem.Equals("Damage") Then
            Return String.Format("When you cast this spell using a spell slot of {0} level or higher, the damage increases by 1{1} for each slot level above {2}.", GetOrderString(level + 1), cboDamageDie.SelectedItem, GetOrderString(level))
        ElseIf level <> 0 And cboCategory.SelectedItem.Equals("Exchange")
            Return String.Format("When you cast this spell using a spell slot of {0} level or higher, the damage increases by 1{1} and the bonus increases by 1{2} for each slot level above {3}.", GetOrderString(level + 1), cboDamageDie.SelectedItem, cboBonusPenaltyDieType.SelectedItem, GetOrderString(level))
        ElseIf level <> 0 And cboTarget.SelectedItem.Contains("Creature")
            Return String.Format("When you cast this spell using a spell slot of {0} level or higher, you can target an additional creature for each slot level above {1}.", GetOrderString(level + 1), GetOrderString(level))
        Else
            Return String.Format("This spell's damage increases by 1{0} when you reach 5th level ({1}{0}), 11th level ({2}{0}), and 17th level ({3}{0}).", cboDamageDie.SelectedItem, CInt(txtDiceAmount.Text) + 1, CInt(txtDiceAmount.Text) + 2, CInt(txtDiceAmount.Text) + 3)
        End If
    End Function

    Private Function StringFromRichTextBox(rtf As RichTextBox) As String
        Dim txtRange As TextRange = New TextRange(rtf.Document.ContentStart, rtf.Document.ContentEnd)
        Return txtRange.Text.Trim()
    End Function

    Private Class Spell
        Public Name As String
        Public Level As Integer
        Public School As String
        Public CastingTime As String
        Public Ritual As Boolean
        Public Range As String
        Public Components As String
        Public Material As String
        Public Duration As String
        Public Concentration As Boolean
        Public Description As String
    End Class

    Private Sub ProcessSrdSpells()
        Dim file As System.IO.StreamReader
        file = My.Computer.FileSystem.OpenTextFileReader("C:\Users\RClough\Documents\Visual Studio 2015\Projects\SpellGenerator\spells.txt")
        Dim currentLine As String
        currentLine = file.ReadLine.Trim()
        Do
            Dim splitLine() As String = currentLine.Split("	"c) ' that's a tab character
            ' Name	Casting Time	Components	Description	Duration	Level	Range	School
            ' 0     1               2           3           4           5       6       7
            Dim current As Spell = New Spell()
            current.Name = splitLine(0)
            current.Level = CInt(splitLine(5))
            current.School = splitLine(7)
            current.CastingTime = splitLine(1)
            current.Components = splitLine(2)
            current.Duration = splitLine(4)
            current.Range = splitLine(6)
            current.Description = splitLine(3)
            If Not gSpells.ContainsKey(LCase(current.Name)) Then gSpells.Add(LCase(current.Name), current)
            ' next line
            currentLine = file.ReadLine.Trim()
        Loop Until currentLine.Equals("END OF SPELLS")
    End Sub

    Private Sub ProcessExtraSpells(filename As String)
        Dim file As System.IO.StreamReader
        file = My.Computer.FileSystem.OpenTextFileReader("C:\Users\RClough\Documents\Visual Studio 2015\Projects\SpellGenerator\" & filename)
        Dim currentLine As String
        currentLine = file.ReadLine.Trim()
        Do
            Dim splitLine() As String = currentLine.Split("	"c) ' that's a tab character
            ' name	level	school	casting_time	ritual	components	material	range	duration	concentration	desc	at_higher_level	class
            ' 0     1       2       3               4       5           6           7       8           9               10      11              12
            Dim current As Spell = New Spell()
            current.Name = splitLine(0)
            If current.Name = "Silvery Barbs" Then
                Dim x = 0
            End If
            current.Level = CInt(splitLine(1))
            current.School = splitLine(2)
            current.CastingTime = splitLine(3)
            current.Ritual = splitLine(4).Contains("TRUE")
            current.Components = splitLine(5)
            current.Material = splitLine(6)
            current.Range = splitLine(7)
            current.Duration = splitLine(8)
            current.Concentration = splitLine(9).Contains("TRUE")
            current.Description = String.Format("{0}{1}", Regex.Replace(splitLine(10), "<p>", vbCrLf), Regex.Replace(splitLine(11), "<p>", vbCrLf))
            current.Description = Regex.Replace(current.Description, "<\/*(b|i)>", "")
            If Not gSpells.ContainsKey(LCase(current.Name)) Then gSpells.Add(LCase(current.Name), current)
            ' next line
            currentLine = file.ReadLine.Trim()
        Loop Until currentLine.Equals("END OF SPELLS")
    End Sub

    Private Sub FindSpellByName(name As String)
        'TODO HERE CURRENTLY
        If gSpells.ContainsKey(name) Then DisplaySpell(gSpells(name))
        lstSuggestedByTyping.Visibility = Visibility.Hidden
    End Sub

    Private Sub FillSpellDescription()
        Dim description As New System.Text.StringBuilder()
        Dim splitText() As String = StringFromRichTextBox(rtfDescription).Split("&")
        For Each fragment As String In splitText
            Select Case fragment
                Case "CREATE"
                    description.Append(WriteDescription())
                Case Else
                    description.Append(fragment)
            End Select
        Next
        txtSpellDescription.Text = description.ToString()
        txtSpellDescription.Text = Regex.Replace(description.ToString(), vbCrLf, vbCrLf & vbCrLf)
    End Sub

    Private Sub FillSpellDescription(strDescription As String)
        If strDescription.Contains(" At Higher Levels. ") Then
            chkUpcast.IsChecked = True
            lblAtHigherLevels.Background = Nothing
            Dim splitText() As String = Regex.Split(strDescription, " At Higher Levels. ")
            txtSpellDescription.Text = splitText(0)
            txtAtHigherLevels.Text = splitText(1)
        ElseIf strDescription.Contains("When you cast this spell using a spell slot of") Then
            chkUpcast.IsChecked = True
            lblAtHigherLevels.Background = Nothing
            Dim splitText() As String = Regex.Split(strDescription, "When you cast this spell using a spell slot of")
            txtSpellDescription.Text = splitText(0)
            txtAtHigherLevels.Text = "When you cast this spell using a spell slot of" & splitText(1)
        ElseIf strDescription.Contains(" This spell’s damage increases ") Then
            chkUpcast.IsChecked = True
            lblAtHigherLevels.Background = Nothing
            Dim splitText() As String = Regex.Split(strDescription, " This spell’s damage increases ")
            txtSpellDescription.Text = splitText(0)
            txtAtHigherLevels.Text = " This spell’s damage increases " & splitText(1)
        ElseIf strDescription.Contains("This spell's damage increases ") Then
            chkUpcast.IsChecked = True
            lblAtHigherLevels.Background = Nothing
            txtSpellDescription.Height = 203
            Dim splitText() As String = Regex.Split(strDescription, "This spell's damage increases ")
            txtSpellDescription.Text = splitText(0)
            txtAtHigherLevels.Text = "This spell's damage increases " & splitText(1)
        Else
            chkUpcast.IsChecked = False
            lblAtHigherLevels.Background = New SolidColorBrush(Colors.White)
            txtSpellDescription.Height = 291
            txtAtHigherLevels.Text = ""
            txtSpellDescription.Text = strDescription
        End If
        If txtSpellDescription.Text.Contains("*  ") Then txtSpellDescription.Text = Regex.Replace(txtSpellDescription.Text, "\*[\s]+", vbCrLf & "•   ")
    End Sub

    Private Sub WriteLevelSchool()
        Dim schoolString As String = "spell"
        If Not cboSchool.SelectedItem Is Nothing Then schoolString = LCase(cboSchool.SelectedItem)
        If cboLevel.SelectedItem Is Nothing Then
            lblLevelSchool.Content = "nth-level " & schoolString
            Return
        End If
        Select Case CInt(cboLevel.SelectedItem)
            Case 0
                lblLevelSchool.Content = schoolString & " cantrip"
            Case Else
                lblLevelSchool.Content = GetOrderString(CInt(cboLevel.SelectedItem)) & "-level " & schoolString
        End Select
    End Sub

    Private Sub WriteLevelSchool(srdSpell As Spell)
        Select Case srdSpell.Level
            Case 0
                lblLevelSchool.Content = LCase(srdSpell.School) & " cantrip"
            Case Else
                lblLevelSchool.Content = GetOrderString(srdSpell.Level) & "-level " & LCase(srdSpell.School)
        End Select
        cboLevel.SelectedIndex = srdSpell.Level
        cboSchool.SelectedItem = srdSpell.School
    End Sub

    Private Function WriteSelfSpellDescription() As String
        'TODO LATER
        Return ""
    End Function

    Private Function WriteDescription() As String
        Dim description As New System.Text.StringBuilder()
        ' RANGE
        Select Case cboRange.SelectedItem
            Case "Self"
                Return WriteSelfSpellDescription()
            ' THESE SPELLS TEND TO BE UNIQUE. A TODO.
            Case "Touch"
                description.Append("You touch a " & LCase(cboTarget.SelectedItem) & ". ")
            Case Else
                description.Append("You choose a " & LCase(cboTarget.SelectedItem) & " within range. ")
        End Select
        ' ATTACK
        Select Case cboAttackSave.SelectedItem
            Case "Melee", "Range"
                description.Append("Make a " & LCase(cboAttackSave.SelectedItem) & " spell attack. On a hit, ")
                If chkSingle.IsChecked Then description.Append("the target ") Else description.Append("Each target ")
            Case "Guaranteed"
                If chkSingle.IsChecked Then description.Append("The target ") Else description.Append("Each target ")
            Case Else
                If chkSingle.IsChecked Then description.Append("The target ") Else description.Append("Each target ")
                If cboShape.SelectedIndex > 0 Then
                    description.Append("within a " & cboSize.SelectedItem & " " & LCase(cboShape.SelectedItem) & " ")
                End If
                description.Append("makes a " & StringifySave(cboAttackSave.SelectedItem) & ". On a failure, ")
                If chkSingle.IsChecked Then description.Append("the target") Else description.Append("Each target")
        End Select
        ' DAMAGE
        Dim needsAnd As Boolean = False
        If cboDamage.SelectedIndex > 0 AndAlso cboDamageDie.SelectedIndex > 0 Then
            description.Append(" takes " & txtDiceAmount.Text.Trim() & cboDamageDie.SelectedItem & " " & LCase(cboDamage.SelectedItem.Trim()) & " damage")
            needsAnd = True
        End If
        ' CONDITION
        If cboInflicts.SelectedIndex > 0 Then
            If needsAnd Then description.Append(" and ")
            description.Append(StringifyCondition(cboInflicts.SelectedItem))
            If Not cboDuration.SelectedItem.Equals("Instantaneous") Then description.Append(" for the duration")
        End If
        description.Append(".")
        ' HALF-DAMAGE
        If chkHalfDamage.IsChecked Then
            description.Append(" On a success, ")
            If chkSingle.IsChecked Then description.Append("the target ") Else description.Append("each target ")
            description.Append("takes half damage")
            If cboInflicts.SelectedIndex > 0 Then description.Append(" and suffers no conditions")
            description.Append(".")
        End If
        ' REPEATABLE
        If chkRepeatable.IsChecked Then description.Append(" At the end of each of its turns, an affected target can make another " & StringifySave(cboAttackSave.SelectedItem) & ". On a success, the effect ends.")
        Return description.ToString()
    End Function

    Private Sub SaveSpellCard() Handles Me.Save
        Dim saveAs As String = String.Format("{0}\SpellCards\{1}.jpg", System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), txtName.Text)
        Dim height As Double = grdCard.RenderSize.Height
        Dim width As Double = grdCard.RenderSize.Width
        Dim renderTarget As RenderTargetBitmap = New RenderTargetBitmap(Int(width), Int(height), 96, 96, PixelFormats.Pbgra32)
        Dim sourceBrush As VisualBrush = New VisualBrush(grdCard)
        Dim drawVisual As DrawingVisual = New DrawingVisual()
        Dim drawContext As DrawingContext = drawVisual.RenderOpen()
        Using drawContext
            drawContext.DrawRectangle(sourceBrush, Nothing, New Rect(New Point(0, 0), New Point(width, height)))
        End Using
        renderTarget.Render(drawVisual)
        Dim jpgEncoder As JpegBitmapEncoder = New JpegBitmapEncoder()
        jpgEncoder.QualityLevel = 100
        jpgEncoder.Frames.Add(BitmapFrame.Create(renderTarget))
        Dim image() As Byte
        Using memoryStream As New MemoryStream()
            jpgEncoder.Save(memoryStream)
            image = memoryStream.ToArray()
        End Using
        System.IO.File.WriteAllBytes(saveAs, image)
    End Sub

    Private Function WriteComponents() As String
        Dim components As New System.Text.StringBuilder
        If chkVerbal.IsChecked Then
            components.Append("V, ")
        End If
        If chkSomatic.IsChecked Then
            components.Append("S, ")
        End If
        If chkMaterial.IsChecked Then
            components.Append("M, ")
        End If
        Dim compString As String = components.ToString()
        If compString = "" Then Return Nothing Else Return compString.Substring(0, compString.Length - 2)
    End Function


    Private Sub FindDamageDice(spellDescription As String)
        Dim match As Match = Regex.Match(spellDescription, "(\d+)d(\d+)")
        If Not match.Success Then
            txtDiceAmount.Text = ""
            cboDamageDie.SelectedIndex = 0
        Else
            txtDiceAmount.Text = match.Groups(1).Value
            cboDamageDie.SelectedItem = "d" & match.Groups(2).Value.Trim()
        End If
    End Sub

    Private Sub FindTarget(spellDescription As String)
        Dim match As Match = Regex.Match(spellDescription, "creature|humanoid|object|point")
        If Not match.Success Then
            cboTarget.SelectedItem = "Self"
        Else
            Dim visible As Boolean = Regex.Match(spellDescription, "\bsee\b").Success
            Dim audible As Boolean = Regex.Match(spellDescription, "\bhear\b").Success
            Dim result As String = TCase(match.Value)
            If visible Then
                result = result & " you can see"
            ElseIf audible
                result = result & " that can hear you"
            End If
            If cboTarget.Items.Contains(result) Then cboTarget.SelectedItem = result
        End If
    End Sub

    Private Sub FindShape(spellDescription As String)
        Dim shapeMatch As Match = Regex.Match(spellDescription, "sphere|cube|cone|cylinder|line")
        Dim selfAreaMatch As Match = Regex.Match(spellDescription, "((extend|surround|blast|erupt|radiate|ripple|around|sweep|from|within \d+ feet)s?).*( from| of)? you\b")
        If shapeMatch.Success AndAlso cboRange.SelectedItem.Equals("Self") Then
            If cboShape.Items.Contains(TCase(shapeMatch.Value)) Then cboShape.SelectedItem = TCase(shapeMatch.Value)
            cboRange.SelectedItem = "Self (Area)"
        ElseIf shapeMatch.Success Then
            If cboShape.Items.Contains(TCase(shapeMatch.Value)) Then cboShape.SelectedItem = TCase(shapeMatch.Value)
        ElseIf selfAreaMatch.Success Then
            cboRange.SelectedItem = "Self (Area)"
            cboShape.SelectedItem = "Sphere"
        End If
        ' Regex pattern for self-centered areas ranges: ((extend|surround|erupt|radiate|ripple|around|sweep|from|within \d+ feet)s?).*( from| of)? you\b
    End Sub

    Private Function TCase(original As String)
        Return UCase(original(0)) + LCase(original.Substring(1))
    End Function

    Private Sub DisplaySpell(srdSpell As Spell)
        lblSpellTitle.Content = srdSpell.Name
        txtName.Text = srdSpell.Name
        SetColorBySchool(srdSpell.School)
        FillSpellDescription(srdSpell.Description)
        rtfDescription.AppendText(srdSpell.Description)
        WriteLevelSchool(srdSpell)
        lblCastingTime.Content = srdSpell.CastingTime
        cboCastingTime.SelectedItem = srdSpell.CastingTime
        ' find target
        FindTarget(srdSpell.Description)
        lblRange.Content = srdSpell.Range
        If cboRange.Items.Contains(srdSpell.Range.Trim()) Then cboRange.SelectedItem = srdSpell.Range Else cboRange.SelectedItem = "Special"
        ' find shape
        FindShape(srdSpell.Description)
        ' find size
        For Each size As String In cboSize.Items
            If srdSpell.Description.Contains(size) Then cboSize.SelectedItem = size
        Next
        chkHalfDamage.IsChecked = srdSpell.Description.Contains("half as much damage")
        chkRepeatable.IsChecked = Regex.IsMatch(srdSpell.Description, "makes? another (.+) saving throw")
        ' TODO: find damage type
        ' find damage dice
        FindDamageDice(srdSpell.Description)
        ' find duration
        If srdSpell.Duration.Contains("Concentration") Then
            chkConcentration.IsChecked = True
            lblDuration.FontSize = 10.5
        Else
            chkConcentration.IsChecked = False
            lblDuration.FontSize = 14.667
        End If
        lblDuration.Content = srdSpell.Duration
        If cboDuration.Items.Contains(srdSpell.Duration.Trim()) Then cboDuration.SelectedItem = srdSpell.Duration.Trim()
        ' find components
        If srdSpell.Components.Contains("V") Then chkVerbal.IsChecked = True Else chkVerbal.IsChecked = False
        If srdSpell.Components.Contains("S") Then chkSomatic.IsChecked = True Else chkSomatic.IsChecked = False
        If srdSpell.Components.Contains("M") Then chkMaterial.IsChecked = True Else chkMaterial.IsChecked = False
        lblComponents.Content = WriteComponents()
        ' TODO: find condition(s)
        ' TODO: find attack/save
    End Sub

    Private Function WriteSelfAreaString() As String
        Dim sizeString As String
        If cboSize.SelectedItem Is Nothing Then sizeString = "Area" Else sizeString = cboSize.SelectedItem
        If cboShape.SelectedItem.Equals("Line") Then
            Dim lineMatch As Match = Regex.Match(sizeString, "\d+ feet long")
            If lineMatch.Success Then sizeString = lineMatch.Value
        End If
        Return "Self (" & sizeString & ")"
    End Function

    Private Sub btnMakeCard_Click(sender As Object, e As RoutedEventArgs) Handles btnMakeCard.Click
        lblSpellTitle.Content = txtName.Text.Trim()
        SetColorBySchool(cboSchool.SelectedItem)
        FillSpellDescription()
        If chkUpcast.IsChecked Then
            lblAtHigherLevels.Background = Nothing
            txtAtHigherLevels.Text = WriteAtHigherLevels()
            txtSpellDescription.Height = 203
        Else
            lblAtHigherLevels.Background = New SolidColorBrush(Colors.White)
            txtAtHigherLevels.Text = ""
            txtSpellDescription.Height = 291
        End If
        WriteLevelSchool()
        lblCastingTime.Content = cboCastingTime.SelectedItem
        lblRange.Content = cboRange.SelectedItem
        If cboRange.SelectedItem.Equals("Self (Area)") Then lblRange.Content = WriteSelfAreaString()
        If chkConcentration.IsChecked Then
            lblDuration.FontSize = 10.5
            lblDuration.Content = "Concentration, up To " & cboDuration.SelectedItem
        Else
            lblDuration.FontSize = 14.667
            lblDuration.Content = cboDuration.SelectedItem
        End If
        lblComponents.Content = WriteComponents()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As RoutedEventArgs) Handles btnSave.Click
        RaiseEvent Save()
    End Sub

    Private Sub btnClearForm_Click(sender As Object, e As RoutedEventArgs) Handles btnClearForm.Click
        txtName.Text = ""
        cboSchool.SelectedIndex = -1
        cboCategory.SelectedIndex = -1
        cboLevel.SelectedIndex = -1
        cboCastingTime.SelectedIndex = -1
        chkRitual.IsChecked = False
        cboDamage.SelectedIndex = 0
        txtDiceAmount.Text = ""
        chkUpcast.IsChecked = False
        cboAttackSave.SelectedIndex = -1
        chkHalfDamage.IsChecked = False
        cboInflicts.SelectedIndex = 0
        cboDuration.SelectedIndex = -1
        chkConcentration.IsChecked = False
        cboTarget.SelectedIndex = -1
        cboRange.SelectedIndex = -1
        cboShape.SelectedIndex = 0
        SetSizesByShape(Nothing)
        cboShape.SelectedIndex = 0
        rtfDescription.SelectAll()
        rtfDescription.Selection.Text = ""
        lstClasses.SelectedItems.Clear()
        chkSingle.IsChecked = False
        chkVerbal.IsChecked = False
        chkSomatic.IsChecked = False
        chkMaterial.IsChecked = False
        chkRepeatable.IsChecked = False
        chkTheNext.IsChecked = False
    End Sub

    Private Sub btnConfirmDisAdv_Click(sender As Object, e As RoutedEventArgs) Handles btnConfirmDisAdv.Click
        grdDisAdvDialogue.Visibility = Visibility.Hidden
    End Sub

    Private Sub cboInflicts_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cboInflicts.SelectionChanged
        If cboInflicts.SelectedItem.Equals("Advantage") Then
            lblAdvDisadv.Content = "Grants Advantage On..."
            grdDisAdvDialogue.Visibility = Visibility.Visible
            lblBonusPenaltyDie.Visibility = Visibility.Hidden
            txtBonusPenaltyDieAmount.Visibility = Visibility.Hidden
            cboBonusPenaltyDieType.Visibility = Visibility.Hidden
        ElseIf cboInflicts.SelectedItem.Equals("Disadvantage") Then
            lblAdvDisadv.Content = "Imposes Disadvantage On..."
            grdDisAdvDialogue.Visibility = Visibility.Visible
            lblBonusPenaltyDie.Visibility = Visibility.Hidden
            txtBonusPenaltyDieAmount.Visibility = Visibility.Hidden
            cboBonusPenaltyDieType.Visibility = Visibility.Hidden
        ElseIf cboInflicts.SelectedItem.Equals("Bonus") Then
            lblAdvDisadv.Content = "Grants Bonus To..."
            grdDisAdvDialogue.Visibility = Visibility.Visible
            lblBonusPenaltyDie.Visibility = Visibility.Visible
            txtBonusPenaltyDieAmount.Visibility = Visibility.Visible
            cboBonusPenaltyDieType.Visibility = Visibility.Visible
        ElseIf cboInflicts.SelectedItem.Equals("Penalty") Then
            lblAdvDisadv.Content = "Imposes Penalty To..."
            grdDisAdvDialogue.Visibility = Visibility.Visible
            lblBonusPenaltyDie.Visibility = Visibility.Visible
            txtBonusPenaltyDieAmount.Visibility = Visibility.Visible
            cboBonusPenaltyDieType.Visibility = Visibility.Visible
        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As RoutedEventArgs) Handles btnSearch.Click
        Dim spellName = LCase(txtName.Text)
        btnClearForm_Click(sender, e)
        FindSpellByName(spellName)
    End Sub

    Private Sub cboShape_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cboShape.SelectionChanged
        Dim current As Shape
        Select Case cboShape.SelectedItem
            Case "—"
                current = Shape.None
            Case "Sphere"
                current = Shape.Sphere
            Case "Cube"
                current = Shape.Cube
            Case "Cone"
                current = Shape.Cone
            Case "Cylinder"
                current = Shape.Cylinder
            Case "Line"
                current = Shape.Line
        End Select
        SetSizesByShape(current)
    End Sub

    Private Sub FillSuggestions()
        If lstSuggestedByTyping Is Nothing Then Return
        lstSuggestedByTyping.Items.Clear()
        For Each spellName As String In gSpells.Keys()
            Dim capsName As String = gSpells(spellName).Name
            If Regex.IsMatch(spellName, LCase(txtName.Text.Trim())) Then lstSuggestedByTyping.Items.Add(capsName)
        Next
    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtName.TextChanged
        If lstSuggestedByTyping Is Nothing Then Return
        FillSuggestions()
        lstSuggestedByTyping.Visibility = Visibility.Visible
    End Sub

    Private Sub HideSuggestions(sender As Object, e As EventArgs) Handles txtName.LostFocus
        If lstSuggestedByTyping Is Nothing Then Return
        lstSuggestedByTyping.Visibility = Visibility.Hidden
    End Sub

    Private Sub lstSuggestedByTyping_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles lstSuggestedByTyping.SelectionChanged
        If Not lstSuggestedByTyping.SelectedItem Is Nothing Then
            txtName.Text = lstSuggestedByTyping.SelectedItem
            btnSearch_Click(sender, e)
            lstSuggestedByTyping.Visibility = Visibility.Hidden
        End If
    End Sub

    Private Sub cboRange_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cboRange.SelectionChanged
        If cboRange.SelectedItem Is Nothing Then Return
        If cboRange.SelectedItem.Equals("Self") Then cboTarget.SelectedItem = "Self"
    End Sub
End Class
