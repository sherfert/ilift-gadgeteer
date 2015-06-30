\NeedsTeXFormat{LaTeX2e}
\ProvidesClass{tk3-team}[2009/10/27 Ingo Sieverdingbeck]
\LoadClass[a4paper]{scrartcl}

% -----------------------------------------------------------------------
% Required packages

\RequirePackage[english]{babel}
\RequirePackage{amssymb}
\RequirePackage{epsfig}
\RequirePackage{graphicx}
\RequirePackage{moreverb}
\RequirePackage{parskip}

% ------------------------------------------------------------------------
% SATIA EDIT

\usepackage[utf8]{inputenc}

% Floats Objekte mit [H] festsetzen
\usepackage{float}
% setzt URL's sch?n mit \url{http://bla.laber.com/~mypage}
\usepackage{url}
\usepackage[colorlinks=true, pdfstartview=FitV, linkcolor=blue,
            citecolor=blue, urlcolor=blue, hyperfigures=true,
            pdftex=true]{hyperref}
            
% Code-Dateien importieren
\usepackage{listings}
\lstset{numbers=left, numberstyle=\tiny, numbersep=5pt}
\lstset{basicstyle=\ttfamily\scriptsize} \lstset{showspaces=false}
\lstset{showtabs=false} \lstset{showstringspaces=false}
\lstset{keywordstyle=\bfseries} \lstset{tabsize=4}
\lstset{frameround=ffff} \lstset{extendedchars=true}
\lstset{stringstyle=\ttfamily} \lstset{commentstyle=\ttfamily}
\lstset{postbreak=\space, breakindent=5pt, breaklines}


% -----------------------------------------------------------------------
% define page layout using typearea

\KOMAoptions{headinclude=true}
\KOMAoptions{footinclude=true}
\areaset{16cm}{27cm}
% -----------------------------------------------------------------------
% define page style
\pagestyle{plain}
\thispagestyle{plain}

% -----------------------------------------------------------------------
% Macro für das Abgabeblatt

\newcommand{\grenzop}[3]{\mathop{#1}\limits^{#2}_{#3}}

\newcommand{\langrechtspfeil}[2]{\grenzop{\longrightarrow}{#1}{#2}}
\newcommand{\bob}[0]{\hspace*{\fill}$\Box$}

% -----------------------------------------------------------------------

\newcommand{\Ex}[8]
{
\begin{tabular}[t]{lr}
  \begin{tabular}[t]{l}
    TK3 Final Project\hspace{20ex} SS 2015\\ \hspace{70ex} \\
    {\Huge\textsf{{#1}}} \hspace{10ex} %\begin{tabular}{c|c|c|c|c|c}1&2&3&4&5&$\sum$\\\hline &&&&&\end{tabular}\\[1.5ex]
    {\Large Submission: {#2}} \\[1ex]
  \end{tabular}
  &
  \begin{tabular}[t]{l}
      {#3}\\\\
      {#4}\\
      {#5}\\
      {#6}\\
      {#7}\\
      {#8}\\
  \end{tabular} \\[2ex]
\end{tabular}\\
\rule{\linewidth}{1pt}
}

%%% Local Variables: 
%%% mode: latex
%%% TeX-master: t
%%% End: 

