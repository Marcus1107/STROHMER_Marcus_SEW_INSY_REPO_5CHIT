from docx import Document
from docx.shared import Cm, Pt, RGBColor
from docx.oxml import OxmlElement
from docx.oxml.ns import qn
from pathlib import Path

OUT = Path(__file__).parent
doc = Document()
sec = doc.sections[0]
sec.page_height, sec.page_width = Cm(29.7), Cm(21)
sec.top_margin = sec.bottom_margin = Cm(1.8)
sec.left_margin = sec.right_margin = Cm(2)
for name in ['Normal','Title','Heading 1','Heading 2']:
    s=doc.styles[name]; s.font.name='Calibri'; s.font.color.rgb=RGBColor(0,0,0)
    s.font.size=Pt(11 if name=='Normal' else 24 if name=='Title' else 19 if name=='Heading 1' else 12)
    s.paragraph_format.space_after=Pt(5)
doc.styles['Normal'].paragraph_format.line_spacing=1.06
doc.styles['Heading 2'].paragraph_format.space_before=Pt(9)
footer=sec.footer.paragraphs[0]; footer.alignment=2
footer.add_run('Textsortenkatalog SRDP  |  ')
field=OxmlElement('w:fldSimple'); field.set(qn('w:instr'),'PAGE'); footer._p.append(field)
def p(t): doc.add_paragraph(t)
def h(t): doc.add_heading(t,2)
def page(t): doc.add_page_break(); doc.add_heading(t,1)
def section(label,text): h(label); p(text)

doc.add_paragraph('Textsortenkatalog SRDP', 'Title')
p('Deutsch · Lernunterlage für die gemeinsame Bearbeitung')
p('Dieser Katalog behandelt die sieben SRDP-Textsorten. Jede Textsorte folgt denselben sechs Kategorien. Die Absätze und Überschriften sind normaler Word-Text und lassen sich direkt in ein gemeinsames Dokument kopieren und dort bearbeiten.')
section('Wortanzahl richtig verwenden','Die folgenden Wortspannen entsprechen dem offiziellen Textsortenkatalog [1]. Verbindlich ist die Spanne in der jeweiligen Prüfungsaufgabe. Die Grenzen enthalten bereits den vorgesehenen Spielraum; nicht zusätzlich zehn Prozent aufschlagen. Die Wortzahlen beziehen sich auf den zu schreibenden Prüfungstext, nicht auf diese Lernunterlage.')
for t in ['Zusammenfassung: 270–330 Wörter','Erörterung: 405–495 oder 540–660 Wörter','Kommentar: 270–330, 405–495 oder 540–660 Wörter','Leserbrief: 270–330 Wörter','Meinungsrede: 405–495 oder 540–660 Wörter','Textanalyse: 405–495 oder 540–660 Wörter','Textinterpretation: 540–660 Wörter']:
    p(t)
section('So verwendest du die Lernhilfe','Die Aufbauschemata und Textbausteine sind Formulierungshilfen, keine starren Prüfungsvorschriften. Bearbeite alle Arbeitsaufträge und passe Reihenfolge, Schwerpunkt und Ton daran an. Ersetze Angaben in eckigen Klammern durch passende Inhalte. Erfinde keine Quellenangaben, Zitate oder Statistiken.')
section('Bedeutung von Plus und Minus','Pro und Kontra bezeichnen Argumente für bzw. gegen eine Position. Zusätzlich zeigen „Passend“ und „Vermeiden“ bei jeder Textsorte, welche Inhalte und Gestaltungsmittel sinnvoll sind.')

entries=[
('Zusammenfassung','270–330 Wörter.',
'Einleitung mit verfügbaren Basisdaten und Thema → Hauptteil mit den geforderten Kernaussagen → gegebenenfalls abschließender Gedanke der Vorlage. Ein eigener bewertender Schluss entfällt.',
'Kein eigenes 3-B-Argumentieren. Pro und Kontra nur wiedergeben, wenn sie in der Vorlage vorkommen und zum Arbeitsauftrag gehören. Passend: Wichtiges auswählen, zusammengehörige Aussagen bündeln, in eigenen Worten formulieren. Vermeiden: persönliche Meinung, Deutung, neue Beispiele und ausführliche Nebendetails.',
'Sachlich und knapp; überwiegend Präsens. Aussagen anderer klar zuordnen, bei indirekter Rede gegebenenfalls Konjunktiv I verwenden. Keine Spannung aufbauen und keine rhetorischen Fragen als eigene Ausschmückung ergänzen.',
'Leitfrage: Was sagt der Ausgangstext? Die Textanalyse untersucht zusätzlich, wie der Text gestaltet ist. Eine Zusammenfassung erklärt keine versteckten Bedeutungen und diskutiert keine eigene Position.',
['Der [Textsorte] „[Titel]“ von [Name], erschienen am [Datum] in [Medium], behandelt [Thema].','Die Autorin beschreibt zunächst [Aspekt].','Als wesentliche Ursache nennt der Text [Ursache].','Dem Beitrag zufolge sei [Aussage].','Abschließend verweist der Autor auf [Schlussgedanke der Vorlage].']),
('Erörterung','405–495 oder 540–660 Wörter.',
'Einleitung mit Problem und Bezug zur Vorlage → Hauptteil mit geordneten Argumenten und Gegenargumenten → Schluss mit begründetem Urteil und gegebenenfalls Lösung oder Ausblick. Mögliches Planschema: Kontra → Pro → Abwägung; alternativ Pro und Kontra nach Gesichtspunkten gegenüberstellen.',
'3-B-Schema für tragende Argumente verwenden. Pro und Kontra abwägen, soweit es die Fragestellung verlangt. Bei einer gerichteten Frage kann eine überwiegend lineare Argumentation passen; relevante Einwände trotzdem bedenken. Passend: Folgen, Bedingungen und Grenzen prüfen. Vermeiden: bloße Meinungslisten, unbelegte Behauptungen und neue Hauptargumente im Schluss.',
'Sachlich, differenziert und logisch verknüpft. Häufig Präsens; Konjunktiv II für Möglichkeiten. Verbindungen wie „allerdings“, „folglich“ und „unter der Voraussetzung, dass“ machen Zusammenhänge sichtbar. Stilmittel sparsam einsetzen; die Begründung trägt den Text.',
'Leitfrage: Welche Position hält einer Abwägung stand? Im Vergleich zum Kommentar steht die systematische Untersuchung stärker im Vordergrund. Ein eigenes begründetes Urteil ist erlaubt und für die Beantwortung der Frage wesentlich.',
['Angesichts von [Problem] stellt sich die Frage, ob [Streitfrage].','Für [Position] spricht, dass [Behauptung].','Dies lässt sich damit begründen, dass [Begründung].','Ein Beispiel dafür wäre [erkennbar hypothetisches Beispiel].','Dem steht der Einwand gegenüber, dass [Gegenargument].','Insgesamt überwiegen [Argumente], weil [Abwägung].']),
('Kommentar','270–330, 405–495 oder 540–660 Wörter.',
'Aussagekräftige Überschrift → zugespitzter Einstieg mit Thema und Anlass → klarer Standpunkt und verdichtete Argumentation → prägnanter Schluss mit Position, Forderung oder Ausblick.',
'3-B als Denkgerüst verwenden, aber sprachlich kompakt umsetzen. Gegenargumente gezielt aufgreifen und prüfen; keine gleich lange Pro-und-Kontra-Liste nötig. Passend: eigenständige Gedanken und ein erkennbarer Standpunkt. Vermeiden: reine Zusammenfassung, pauschale Angriffe und Zuspitzungen ohne Begründung.',
'Pointiert, anschaulich und meinungsbetont; meist Präsens. „Ich“ in der Regel vermeiden. Geeignet sind etwa Antithesen, kurze Sätze und rhetorische Fragen. Ironie nur so einsetzen, dass Aussage und Haltung verständlich bleiben.',
'Leitfrage: Wie beurteile ich dieses öffentliche Thema überzeugend? Der Kommentar ist journalistisch zugespitzt. Der Leserbrief reagiert in Briefform auf eine Veröffentlichung; die Erörterung wägt ausführlicher und systematischer ab.',
['[Entwicklung] ist längst mehr als [verharmlosende Bezeichnung].','Entscheidend ist nicht allein [Aspekt], sondern auch [Aspekt].','Wer [Forderung] verlangt, muss auch [Folge] berücksichtigen.','Zwar trifft zu, dass [Einwand]. Dennoch [begründete Entgegnung].','Deshalb braucht es [konkrete Konsequenz].']),
('Leserbrief','270–330 Wörter.',
'Anrede → Schreibanlass mit Artikelbezug → kurze Reaktion auf relevante Aussagen → eigene begründete Position → Fazit oder Appell → Grußformel und Name. Ein vollständiger Briefkopf ist bei der SRDP nicht nötig.',
'Wenige ausgewählte Argumente nach dem 3-B-Schema. Zustimmung, Ablehnung oder Ergänzung sind möglich. Pro und Kontra nur so weit, wie sie deine Reaktion erklären. Passend: konkret auf eine Aussage des Artikels eingehen. Vermeiden: den ganzen Artikel nacherzählen, abschweifen oder Personen beleidigen.',
'Persönlich, höflich und bestimmt; Ich-Form möglich. Überwiegend Präsens, für eigene vergangene Erfahrungen passende Vergangenheitsformen. Einzelne rhetorische Fragen oder Kontraste können die Position unterstützen.',
'Leitfrage: Was möchte ich als Leserin oder Leser zu diesem Beitrag sagen? Der konkrete Veröffentlichungsbezug und die Briefform unterscheiden den Leserbrief vom Kommentar. Er ist keine private Nachricht an eine befreundete Person.',
['Sehr geehrte Redaktion,','zu Ihrem Beitrag „[Titel]“ von [Name], erschienen am [Datum], möchte ich Stellung nehmen.','Der Aussage, dass [Aussage], stimme ich zu, weil [Grund].','Nicht nachvollziehbar erscheint mir dagegen [Aspekt], da [Grund].','Ich wünsche mir daher, dass [konkretes Anliegen].','Mit freundlichen Grüßen\n[Name]']),
('Meinungsrede','405–495 oder 540–660 Wörter.',
'Passende Anrede und Aufmerksamkeit weckender Einstieg → Anlass und eigene Position → anschauliche Argumente mit Publikumsbezug → Schluss mit zentraler Botschaft und konkretem Appell; gegebenenfalls Dank.',
'3-B-Argumente so formulieren, dass man ihnen beim Zuhören folgen kann. Einwände aufgreifen, wenn sie für das Publikum relevant sind. Passend: alltagsnahe Beispiele, direkte Ansprache und nachvollziehbare Handlungsaufforderung. Vermeiden: bloße Parolen, erfundene Zahlen und lange theoretische Abschweifungen.',
'Mündlich gut verständliche Standardsprache, eher kurze Sätze. „Ich“, „wir“ und direkte Anrede passend zur Rolle verwenden. Wiederholung, Anapher, rhetorische Frage und Dreierfigur können Gedanken einprägsam machen. Stilmittel ersetzen keine Argumente.',
'Leitfrage: Wie überzeuge ich dieses Publikum bei diesem Anlass? Die Rede ist auf Zuhörende ausgerichtet. Ein Kommentar richtet sich an Lesende; eine reine Informationsrede will vor allem informieren und vertritt nicht zwingend eine Position.',
['Liebe Mitschülerinnen und Mitschüler, sehr geehrte Lehrkräfte!','Stellen Sie sich vor, [kurze passende Situation].','Warum betrifft uns [Thema] alle?','Ein entscheidender Grund dafür ist [Argument].','Manche von Ihnen werden einwenden, dass [Einwand]. Dazu möchte ich sagen: [Antwort].','Lassen Sie uns deshalb [konkreter Handlungsvorschlag].']),
('Textanalyse','405–495 oder 540–660 Wörter.',
'Einleitung mit Basisdaten und Thema → knappe inhaltliche Orientierung → Untersuchung von Aufbau, Argumentation und Sprache entsprechend dem Auftrag → Schluss mit gebündelten Ergebnissen zur Textgestaltung und Funktion.',
'Kein eigenes Pro und Kontra zum Sachthema. Stattdessen am Text arbeiten: Beobachtung → Beleg → Erklärung der Funktion. Passend: Wortwahl, Satzbau, Argumenttypen, Adressierung und Stilmittel in ihrem Zusammenhang untersuchen. Vermeiden: Stilmittel nur aufzählen, Wirkungen pauschal behaupten oder persönlich über das Thema urteilen.',
'Sachlich und präzise, meist Präsens. Fachbegriffe richtig anwenden. Kurze Zitate mit Zeilenangaben einbauen, wenn die Vorlage Zeilen vorgibt. Mögliche Wirkung vorsichtig und konkret beschreiben: „kann … hervorheben“ statt „bewirkt bei allen …“.',
'Leitfrage: Wie funktioniert dieser nicht fiktionale Text? Anders als die Zusammenfassung untersucht die Analyse die Gestaltung. Anders als die literarische Interpretation entwickelt sie keine symbolische Gesamtdeutung.',
['Der [Textsorte] „[Titel]“ von [Name] thematisiert [Thema].','Der Text lässt sich in [Anzahl] inhaltliche Abschnitte gliedern.','Die Argumentation geht von [These] aus und stützt sich auf [Belegtyp].','Die Formulierung „[Zitat]“ (Z. [Zahl]) enthält eine [Stilmittel].','Dadurch wird [konkreter Aspekt] hervorgehoben, was die Aussage [Aussage] unterstützt.','Insgesamt dient die sprachliche Gestaltung vor allem dazu, [Textfunktion].']),
('Textinterpretation','540–660 Wörter.',
'Einleitung mit Basisdaten, Thema und möglicher Deutungshypothese → kurze Inhaltsangabe → Analyse und Deutung miteinander verbinden → Schluss mit Ergebnis und Rückbezug auf die Deutungshypothese.',
'Deutungsbehauptung → Textbeleg → Erklärung. Kein gesellschaftliches Pro und Kontra als Ersatz für die Textarbeit. Je nach Gattung: Erzählperspektive und Figuren; lyrisches Ich, Verse und Klang; oder Dialog und Konflikt untersuchen. Passend: mehrere Textstellen miteinander verknüpfen. Vermeiden: Nacherzählung, unbelegte Symboldeutung und erfundene biografische Hintergründe.',
'Sachlich erklärend und überwiegend im Präsens. Begriffe wie Erzähler, lyrisches Ich, Motiv oder Metapher gezielt nutzen. Autor und Erzähler bzw. lyrisches Ich auseinanderhalten. Deutungen als begründete Lesart ausdrücken; Zitate korrekt kennzeichnen.',
'Leitfrage: Welche Bedeutung lässt sich aus der Gestaltung dieses literarischen Textes erschließen? Die Interpretation baut auf Analyse auf und entwickelt daraus eine Deutung. Sie ist weder freie Fantasie noch ein persönlicher Gefallensbericht.',
['Der literarische Text „[Titel]“ von [Name] aus dem Jahr [Jahr] behandelt [Thema].','Der Text lässt sich als Auseinandersetzung mit [Deutung] lesen.','Dafür spricht die Formulierung „[Zitat]“ (Z. [Zahl]).','Das wiederkehrende Motiv [Motiv] kann hier für [Bedeutung] stehen, weil [Textbezug].','Die Erzählperspektive beschränkt den Blick auf [Wahrnehmung].','Die untersuchten Merkmale stützen somit die Deutung, dass [Ergebnis].'])
]
for i,(name,w,auf,inh,spr,abg,bausteine) in enumerate(entries,1):
    page(f'{i} {name}')
    for label,text in [('Wortanzahl',w),('Aufbau schematisch',auf),('Inhalt und Argumentation',inh),('Sprachliche Merkmale',spr),('Abgrenzung',abg)]: section(label,text)
    h('Textbausteine')
    for b in bausteine: p('„'+b+'“')

page('Argumentieren und Stilmittel')
section('Das 3 B Schema','Behauptung → Begründung → Beispiel oder Beleg. Es ist eine Lernhilfe für nachvollziehbare Argumente. Nicht jeder Satz benötigt alle drei Teile; ein tragendes Argument sollte jedoch verständlich begründet und gestützt werden.')
p('Behauptung: „Die Schulbibliothek sollte länger geöffnet sein.“\nBegründung: „Denn manche Lernende benötigen nach dem Unterricht einen ruhigen Arbeitsplatz.“\nBeispiel: „Wer zu Hause ein Zimmer mit Geschwistern teilt, könnte dort ungestört eine Präsentation vorbereiten.“')
p('Dieses Beispiel veranschaulicht den Gedanken, beweist aber nicht seine Häufigkeit. Ein belastbarer Beleg wäre etwa eine tatsächlich vorliegende Erhebung zum Bedarf. Zahlen und Studien niemals erfinden.')
section('Pro und Kontra verbinden','„Für [Vorschlag] spricht [Argument]. Dagegen lässt sich einwenden, dass [Einwand]. Dieser Einwand wiegt besonders schwer, wenn [Bedingung]. Unter [andere Bedingung] überwiegt jedoch [Argument], weil [Begründung].“')
section('Stilmittel mit eigenen Beispielen','Rhetorische Frage: „Wie lange wollen wir noch warten?“ – fordert gedankliche Beteiligung ein.\nAnapher: „Wir brauchen Zeit. Wir brauchen Raum. Wir brauchen Unterstützung.“ – gleicher Satzanfang betont die Forderungen.\nAntithese: „Heute sparen, morgen draufzahlen.“ – macht einen Gegensatz deutlich.\nMetapher: „Ein Berg von Aufgaben.“ – veranschaulicht eine große Menge.\nDreierfigur: „klar, fair und umsetzbar“ – bündelt drei Eigenschaften.\nEllipse: „Keine Zeit. Kein Platz.“ – verkürzt den Satz und kann Dringlichkeit vermitteln.')
section('Stilmittel sinnvoll nutzen','In Kommentar und Meinungsrede selbst gezielt einsetzen; im Leserbrief dosiert verwenden. In Textanalyse und Textinterpretation die Mittel der Vorlage untersuchen und mit Inhalt und Funktion verbinden. In der Zusammenfassung keine eigene rhetorische Ausschmückung ergänzen. Die Wirkung hängt immer vom konkreten Zusammenhang ab.')
section('Kurzer Check vor der Abgabe','Sind alle Arbeitsaufträge erfüllt? Stimmen Textsorte, Rolle und Adressaten? Ist die Textbeilage korrekt einbezogen? Sind eigene Gedanken und fremde Aussagen unterscheidbar? Haben Absätze einen klaren Schwerpunkt? Passen Wortanzahl, Grammatik, Rechtschreibung und Zeichensetzung?')

page('Quellen und gemeinsame Bearbeitung')
p('Die Wortspannen und die Auswahl der sieben Textsorten wurden mit dem offiziellen Katalog abgeglichen. Die Aufbauskizzen, Merkhilfen und Beispielsätze sind eigenständig formulierte Lernvorschläge. Die konkrete Aufgabenstellung hat Vorrang.')
h('Offizielle Quellen')
p('[1] Bundesministerium / matura.gv.at: Textsortenkatalog zur SRDP in der Unterrichtssprache, Fassung 2020. Textsortenprofile auf den Seiten 12–18. Abgerufen am 15. September 2026.')
p('https://www.matura.gv.at/index.php?eID=dumpFile&f=4525&t=f&token=950c7f2b86f0ebc3459c5f0aa0e04013ab99c572')
p('[2] matura.gv.at: Deutsch, Prüfung vom 20. September 2023, Seite 2. Beispiele für die aufgabenspezifischen Wortspannen.')
p('https://www.matura.gv.at/fileadmin/user_upload/downloads/Matura-2022-23/DEU/KL23_PT2_ALL_DEU_SR_CC_AU.pdf')
p('[3] matura.gv.at: Unterrichtssprache. Offizielle Übersicht zu Prüfung und Materialien.')
p('https://www.matura.gv.at/srdp/unterrichtssprache')
h('In euer gemeinsames Dokument übernehmen')
p('Markiere die benötigten Abschnitte in Word und kopiere sie in euer Teamdokument. Beim Einfügen kannst du „Zielformatvorlagen verwenden“ wählen, damit die Inhalte dessen Gestaltung übernehmen. Alle Inhalte sind frei bearbeitbar; es gibt keine Textfelder oder eingebetteten Textbilder.')
p('Für die Teamarbeit könnt ihr die Textsorten untereinander aufteilen, Beispiele ergänzen und offene Fragen als Kommentare markieren. Gleiche Änderungen an Wortspannen oder Pflichtmerkmalen immer mit der Aufgabenstellung bzw. der offiziellen Quelle ab.')
doc.core_properties.title='Textsortenkatalog SRDP Deutsch'
doc.core_properties.subject='Lernunterlage mit Aufbau, Sprache und Textbausteinen'
doc.save(OUT/'Textsortenkatalog_SRDP.docx')
print(OUT/'Textsortenkatalog_SRDP.docx')
