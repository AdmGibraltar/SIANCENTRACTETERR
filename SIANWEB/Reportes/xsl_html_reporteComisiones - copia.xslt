<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:js="urn:extra-functions">
	<xsl:output method="xml" indent="yes"/>
    <xsl:template match="Data">
			<html>
				<head>
					<title>Reporte de comisiones para representante de ventas</title>
					<meta http-equiv="content-type" content="text/html; charset=UTF-8"></meta>
					<style type="text/css">
						table { empty-cells: show; border-spacing: 0px; margin: 0px; padding: 0px;}
						.pagebreak {page-break-after: always;}
						.tableReportHeader{border-top: solid DarkBlue 1px; border-left: solid DarkBlue 1px; border-right: solid DarkBlue 1px; width: 845px;}
						.tabledetails{ width: 845px; }
						.tableReportFooter{bottom: 2px;border-bottom: solid DarkBlue 1px;  width: 845px;}
						.imglogo{border-style: none; vertical-align: top; border-color: White;}
						td{vertical-align: top; font-family: Arial, Helvetica, sans-serif; font-size: 9pt}
						.tdmargin{width:10px;}
						.documentheader{font-family:Arial; font-size:9pt; color:DarkBlue; font-weight:bold;}
						.negrita{font-family:Arial; font-size:9pt; color:Black; font-weight:bold;}
						th{font-family:Arial; font-size:8pt; color:black;  text-align:center;border: solid 1px darkblue;}
					
						.documenttotal{font-family:Arial; font-size:9pt; color:DarkBlue; font-weight:bold; }
						.tdtotalmargin{width:450px;}
				
						.tooltip {
							position: relative;
							display: inline-block;
						}

						.tooltip .tooltiptext {
							visibility: hidden;
							width: 210px;
							background-color: #555;
							color: #fff;
							text-align: center;
							border-radius: 6px;
							padding: 5px 0;
							position: absolute;
							z-index: 1;
							bottom: 125%;
							left: 50%;
							margin-left: -60px;
							opacity: 0;
							transition: opacity 1s;
						}

						.tooltip .tooltiptext::after {
							content: "";
							position: absolute;
							top: 100%;
							left: 50%;
							margin-left: -5px;
							border-width: 5px;
							border-style: solid;
							border-color: #555 transparent transparent transparent;
						}

						.tooltip:hover .tooltiptext {
							visibility: visible;
							opacity: 1;
						}
					</style>

				</head>
				
				<body>

					<xsl:copy-of select="$ReportHeader"/>

					<xsl:for-each select="Cliente">

						<xsl:call-template name="Filler">
							<xsl:with-param name="fillercount" select="1" />
						</xsl:call-template>

						<!-- <xsl:copy-of select="$ClienteRecipient"/> -->

						<xsl:call-template name="Filler">
							<xsl:with-param name="fillercount" select="1" />
						</xsl:call-template>

						<!-- <xsl:copy-of select="$ClienteHeader"/> -->

						
						<table class="tableReportHeader" cellspacing="0">
															<tr>
																 
																<td class="documentheader"  align="left">
																	Cliente :
																	<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
																</td>
																<td class="negrita">
																	<xsl:value-of select="Cte_NomComercial" />
																	<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
																	 
																</td>
																 
							 								</tr>
						 
								</table>


						<xsl:call-template name="Filler">
							<xsl:with-param name="fillercount" select="1" />
						</xsl:call-template>

						<xsl:copy-of select="$ClienteDetallesHeader"/>

						<xsl:for-each select="ClienteDetalles/ClienteDetalle">

							<table class="tabledetails" cellspacing="0" style="table-layout:fixed">
								<tr>
									<td class="tdmargin" />
									<td style="width:40px" align="right"  >
										<xsl:value-of select="Pag_Referencia" />
										<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
									</td>
									<td   style="width:60px" align="center"  >
										<xsl:value-of select="Vencimiento" />
										<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
									</td>
									<td   style="width:60px" align="center" >
										<xsl:value-of select="FechaPago" />
										<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
									</td>
									<td style="width:30px" align="right" >
										<xsl:value-of select="Dias" />
										<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
									</td>
									<td style="width:50px" align="right"  >
										<xsl:value-of select="concat('$ ', format-number(Importe,  '###,##0.00'))" />
										<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
									</td>
									<td style="width:50px" align="right"  >
										<xsl:value-of select="concat('$ ',format-number(UP,  '###,##0.00'))" />
										<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
									</td>
									<td style="width:60px" align="right"  >
										<xsl:value-of select=" format-number(Mult_Porc,  '###,##0.00')" />
										<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
									</td>
									<td style="width:50px" align="right"  >
										<xsl:value-of select="concat('$ ', format-number(AjCobranza,  '###,##0.00'))" />
										<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
									</td>
									<td class="tdmargin" />
<!-- EJEMPLO DE COMO PONER FORMATO DE PORCENTAJE 
                     <td style="width:50px" align="right" class="blueline">
										<xsl:value-of select="concat(UP, ' %')" />
										<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
									</td> -->
									
								</tr>
							</table>
							<xsl:choose>
								<!-- case of only one page-->
								<xsl:when test="(position() mod 26 = 0 and position() = 26)">
									<!--40 rows per page-->
									<xsl:call-template name="Filler">
										<xsl:with-param name="fillercount" select="1" />
									</xsl:call-template>

									<xsl:copy-of select="$ReportFooter" />

									<!-- <br class="pagebreak" JFCV comentado 30mayo />-->

									<xsl:copy-of select="$ReportHeader" />

									<xsl:call-template name="Filler">
										<xsl:with-param name="fillercount" select="2" />
									</xsl:call-template>

									<xsl:copy-of select="$ClienteDetallesHeader"/>
									
								</xsl:when>
								<!-- case of more than one page-->
								<xsl:otherwise>
									<xsl:if test="(26 - position() mod 46) = 0 and position() &gt; (26 + position() mod 26)">
										<!--46 rows per page-->
										<xsl:call-template name="Filler">
											<xsl:with-param name="fillercount" select="1" />
										</xsl:call-template>

										<xsl:copy-of select="$ReportFooter" />

										<!-- <br class="pagebreak"  JFCV comentado 30mayo />-->

										<xsl:copy-of select="$ReportHeader" />

										<xsl:call-template name="Filler">
											<xsl:with-param name="fillercount" select="2" />
										</xsl:call-template>

										<xsl:copy-of select="$ClienteDetallesHeader"/>

									</xsl:if>

								</xsl:otherwise>
							</xsl:choose>


						</xsl:for-each>

								<!-- totales del detalle por cliente  -->
						<table class="tabledetails" cellspacing="0"  >  

							<tr>
									<td class="tdmargin" />
									<td style="width:40px" align="right"  >
										
									</td>
									<td   style="width:60px" align="center"  >
									
									</td>
									<td   style="width:60px" align="center" >
										
									</td>
									<td style="width:30px" align="right" >
										
									</td>
									<td  class="negrita"  style="width:50px" align="right"  >
									______________
									</td>
									<td  class="negrita"  style="width:50px" align="right"  >
									___________
									</td>
									<td style="width:60px" align="right"  >
										 
									</td>
									<td  class="negrita" style="width:50px" align="right"  >
									___________
									</td>
									<td class="tdmargin" />

								 
								</tr>

								<tr>
									<td class="tdmargin" />
									<td style="width:40px" align="right"  >
										
									</td>
									<td   style="width:60px" align="center"  >
									
									</td>
									<td   style="width:60px" align="center" >
										
									</td>
									<td  class="negrita" style="width:30px" align="right" >
										Totales
										<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
									</td>
									<td  class="negrita" style="width:50px" align="right"  >
										 <xsl:value-of select="concat('$ ',format-number(sum(ClienteDetalles/ClienteDetalle/Importe),  '###,##0.00'))" /> <!-- <xsl:value-of select="concat('$ ', Importe)" /> -->
										<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
									</td>
									<td  class="negrita" style="width:50px" align="right"  >
										<xsl:value-of select="concat('$ ',format-number(sum(ClienteDetalles/ClienteDetalle/UP),  '###,##0.00'))" />
										<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
									</td>
									<td style="width:60px" align="right"  >
										 
									</td>
									<td  class="negrita" style="width:50px" align="right"  >
										<xsl:value-of select="concat('$ ',format-number(sum(ClienteDetalles/ClienteDetalle/AjCobranza),  '###,##0.00'))" />
									</td>
									<td class="tdmargin" />

								 
								 
								</tr>
						</table>


<!--     lISTO EL SUBTOTAL ESPECIAL -->
<br></br>
 				<table class="tabledetails" cellspacing="0"  >  <!-- style="table-layout:fixed"> -->
								<tr>
									 <td class="tdmargin" />
									<td style="width:48px"   >
														</td>

									<td style="width:120px">
										Categoría
									</td> 
									<td style="width:30px">
										 % de Participación
									</td>
									<td style="width:50px">
										 $ Utilidad
									</td>
									<td style="width:50px">
										 % En la mezcla
									</td>
									<td style="width:60px">
										 Ponderación
									</td>
								 
								</tr>
				</table>


						<xsl:for-each select="PlanesEspeciales/PlanEspecial">
							<table class="tabledetails" cellspacing="0"  >  <!-- style="table-layout:fixed"> -->
								<tr>
									<td class="tdmargin" />
									<td style="width:40px" align="left" ></td>
									 
									<td   style="width:120px" align="left"  >
										<xsl:value-of select="Cat_Descripcion" />
										<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
									</td>
									 
									<td style="width:30px" align="right" >
										<xsl:value-of select="Cat_Participacion" />
										<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
									</td>
									<td style="width:50px" align="right" >
										<xsl:value-of select="concat('$ ', Utilidad)" />
										<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
									</td>
									<td style="width:50px" align="right" >
										<xsl:value-of select="concat('$ ',Mezcla)" />
										<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
									</td>
									<td style="width:60px" align="right" >
										<xsl:value-of select="concat('$ ', Ponderacion)" />
										<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
									</td>
									<td class="tdmargin" />

									
								</tr>
							</table>
							<xsl:choose>
								<!-- case of only one page-->
								<xsl:when test="(position() mod 40 = 0 and position() = 40)">
									<!--40 rows per page-->
									<xsl:call-template name="Filler">
										<xsl:with-param name="fillercount" select="1" />
									</xsl:call-template>

									<xsl:copy-of select="$ReportFooter" />

									<!-- <br class="pagebreak" JFCV comentado 30mayo />-->

									<xsl:copy-of select="$ReportHeader" />

									<xsl:call-template name="Filler">
										<xsl:with-param name="fillercount" select="2" />
									</xsl:call-template>

									<!-- <xsl:copy-of select="$PlanEspecialHeader"/> -->
									
								</xsl:when>
								<!-- case of more than one page-->
								<xsl:otherwise>
									<xsl:if test="(40 - position() mod 46) = 0 and position() &gt; (40 + position() mod 40)">
										<!--46 rows per page-->
										<xsl:call-template name="Filler">
											<xsl:with-param name="fillercount" select="1" />
										</xsl:call-template>

										<xsl:copy-of select="$ReportFooter" />

										<!-- <br class="pagebreak"  JFCV comentado 30mayo/>-->

										<xsl:copy-of select="$ReportHeader" />

										<xsl:call-template name="Filler">
											<xsl:with-param name="fillercount" select="2" />
										</xsl:call-template>

										<!-- <xsl:copy-of select="$PlanEspecialHeader"/> -->

									</xsl:if>

								</xsl:otherwise>
							</xsl:choose>
 
 
						</xsl:for-each>   <!-- PlanesEspeciales  --> 

			<!-- Porcentaje de Participación  --> 
			<table class="tabledetails" cellspacing="0"  >  <!-- style="table-layout:fixed"> -->
								<tr>
									<td class="tdmargin" />
									<td style="width:40px"   > </td>

									<td style="width:120px"></td> 
									<td style="width:47px"></td>
									<td class="negrita" style="width:45px" align="right" >
										 _____________
									</td>
									<td  class="negrita" style="width:45px" align="center" >
										 _____________
									</td>
									<td style="width:60px"></td>
								 
								</tr>
								<tr>
									 <td class="tdmargin" />
									<td style="width:45px"   ></td>

									<td style="width:120px"></td> 
									<td style="width:47px"></td>
									<td  class="negrita" style="width:45px" align="right" >
										 <xsl:value-of select="format-number(sum(PlanesEspeciales/PlanEspecial/Utilidad),  '###,##0.00')" />
									</td>
									<td  class="negrita" style="width:45px" align="center" >
										 <xsl:value-of select="format-number(sum(PlanesEspeciales/PlanEspecial/Mezcla),  '##0.00')" />
												
									</td>
									
									<td style="width:60px"></td>
								 
								</tr>
			</table>
			<br></br>
								<table class="tabledetails" cellspacing="0"  >

										<tr>
											 <td class="tdmargin" />
											<td style="width:40px"   >
																</td>

											<td  class="negrita" style="width:190px">
												 Porcentaje de participación ponderado
											</td> 
											<td style="width:15px">
												 
											</td>
											<td style="width:10px">
												  
											</td>
											<td style="width:35px">
												  
											</td>
											<td  class="negrita" style="width:60px">
												  <xsl:value-of select="format-number(/Data/Parametros/ValorPPPP,  '##0.00')" />
											</td>
										 
										</tr>


								</table>


								<!--  AQUI VA EL ESTADO DE RESULTADOS DEL CLIENTE -->

								<table class="tableedoresconencabezado" cellspacing="0"  >  <!-- style="table-layout:fixed"> -->
									<tr>
										<td class="tdmargin" />
										<td style="width:415px" align="left" ></td>
										<td style="width:350px" align="center" >
											<a href="" title="Estado de resultados del cliente" >Estado de resultados del cliente</a>
										</td>
										<td style="width:100px" align="right" ></td>
										<td class="tdmargin" />
									</tr>
								</table>
 
								<table class="tableedorescon" cellspacing="0"  >  
									<tr>
										<td style="width:335px" align="left" ></td>
										<td style="width:350px" align="left" >
											Importe de venta cobrada
										</td>
										<td style="width:110px" align="right" >
											<xsl:value-of select="format-number(CteVtaCob, '###,##0.00') " />
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
									</tr>
									<tr>
										<td style="width:335px" align="left" >
										</td>
										<td style="width:350px" align="left" >
											Utilidad prima
										</td>
										<td style="width:110px" align="right" >
											<xsl:value-of select="format-number(CteUP, '###,##0.00')" />
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
									</tr>
									<tr>
										<td style="width:335px" align="left" >
										</td>
										<td style="width:350px" align="left" >
											Amortización de sistemas propietarios
										</td>
										<td style="width:110px" align="right" >
											<xsl:value-of select="format-number(CteAN, '###,##0.00')" />
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
									</tr>
									<tr>
										<td style="width:335px" align="left" >
										</td>
										<td style="width:350px" align="left" >
											Gastos de técnico de servicio 
										</td>
										<td style="width:110px" align="right" >
											<xsl:value-of select="format-number(CteGST, '###,##0.00')" />
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
									</tr>
									<tr>
										<td style="width:345px" align="left" >
										</td>
										<td style="width:350px" align="left" >
											Amortización anticipada de sistemas
										</td>
										<td style="width:120px" align="right" >
											<xsl:value-of select="format-number(CteAA, '###,##0.00')" />
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
									</tr>
									<tr>
										<td style="width:345px" align="left" >
										</td>
										<td style="width:350px" align="left" >
											Mano de obra en proyectos
										</td>
										<td style="width:120px" align="right" >
											<xsl:value-of select="format-number(CteMO, '###,##0.00')" />
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
									</tr>
									<tr>
										<td style="width:345px" align="left" >
										</td>
										<td style="width:350px" align="left" >
											Amortización de equipos arrendados
										</td>
										<td style="width:120px" align="right" >
											<xsl:value-of select="format-number(CteAE, '###,##0.00')" />
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
									</tr>

									<tr>
										<td style="width:345px" align="left" >
										</td>
										<td style="width:350px" align="left" >
											_____________________________________ 
										</td>
										<td style="width:120px" align="right" >
											 
										</td>
									</tr>

									<tr>
										<td style="width:345px" align="left" >
										</td>
										<td class="negrita"  style="width:350px" align="left" >
											Utilidad Bruta 
										</td>
										<td class="negrita"  style="width:120px" align="right" >
											<xsl:value-of select="format-number( CteUP - CteAN 
												- CteGST - CteAA - CteMO - CteAE, '###,##0.00')"/>
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
									</tr>

									<tr>
										<td style="width:345px" align="left" >
										</td>
										<td style="width:350px" align="left" >
											 <br></br>
										</td>
										<td style="width:120px" align="right" >
											 
										</td>
									</tr>

									<tr>
										<td style="width:345px" align="left" >
										</td>
										<td style="width:350px" align="left" >
											Factor por cobranza
										</td>
										<td style="width:120px" align="right" >
											<xsl:value-of select="format-number(CteAjCobranza, '###,##0.00')" />
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
									</tr>

									<tr>
										<td style="width:345px" align="left" >
										</td>
										<td style="width:350px" align="left" >
											Utilidad bruta del cliente ajustada por cobranza
										</td>
										<td style="width:120px" align="right" >
											<xsl:value-of select="format-number(CteUBAj ,'###,##0.00')"/>
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
									</tr>
									<tr>
										<td style="width:345px" align="left" >
										</td>
										<td style="width:350px" align="left" >
											Factor por porcentaje de participacion ponderado1
										</td>
										<td style="width:120px" align="right" >
											<xsl:value-of select="format-number(CtePPPA ,'###,##0.000')"/>
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
									</tr>
									<tr>
										<td style="width:345px" align="left" >
										</td>
										<td style="width:350px" align="left" >
											 <br></br>
										</td>
										<td style="width:120px" align="right" >
											 
										</td>
									</tr>

									<tr>
										<td style="width:345px" align="left" >
										</td>
										<td style="width:350px" align="left" >
											Comisión preliminar
										</td>
										<td style="width:120px" align="right" >
											<xsl:value-of select="format-number(CteCP, '###,##0.00')"/>
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
									</tr>

									<tr>
										<td style="width:345px" align="left" >
										</td>
										<td style="width:350px" align="left" >
											Factor por rentabilidad global (0-1)
										</td>
										<td style="width:120px" align="right" >
											<xsl:value-of select="format-number(CteFR, '###,##0.00')"/>
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
									</tr>
									<tr>
										<td style="width:345px" align="left" >
										</td>
										<td style="width:350px" align="left" >
											_____________________________________ 
										</td>
										<td style="width:120px" align="right" >
											 
										</td>
									</tr>
									<tr>
										<td style="width:345px" align="left" >
										</td>
										<td class="negrita" style="width:350px" align="left" >
											Comisión base
										</td>
										<td class="negrita" style="width:120px" align="right" >
											<xsl:value-of select="format-number(CteCB, '###,##0.00') " />
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
									</tr>


						</table>


				</xsl:for-each>		<!-- Cliente  --> 
			<br></br>

					<!-- Edo resultados -->

					<table class="tableedoresconencabezado" cellspacing="0"  >  <!-- style="table-layout:fixed"> -->
								<tr>
									<td class="tdmargin" />
									<td style="width:415px" align="left" >
									</td>
									<td style="width:350px" align="center" >
										<a href="" title="Estado de Resultados Consolidado" >Estado de resultados Consolidado</a>
									</td>
									<td style="width:100px" align="right" >
										 
									</td>
									 
									<td class="tdmargin" />
									
								</tr>
					</table>
 
						
							<!-- ##ESTADORESULTADOSCONSOLIDADO##-->
							<xsl:for-each select="ConceptosPredefinidos/ConceptosPredefinido">
									<table class="tableedorescon" cellspacing="0"  >  
										{0}
									</table>
						</xsl:for-each>		<!-- edoresultados consolidado  --> 
						
						<!-- <xsl:variable name="lista">55</xsl:variable>-->
						<!-- <xsl:for-each select="/Data/EdoResultadosConRows/EdoResultadosCon">
							<table class="tableedorescon" cellspacing="0"  > -->  <!-- style="table-layout:fixed"> -->
								<!-- <tr>
									<td class="tdmargin" />
									<td style="width:335px" align="left" >
									</td>
									<td style="width:350px" align="left" >
										<xsl:value-of select="Texto" />
										<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
									</td>
									<td style="width:110px" align="right" >
										<xsl:value-of select="Valor" />
										<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
									</td>
									 
									<td class="tdmargin" /> -->

									<!-- <xsl:value-of select="$lista+10"   <xsl:value-of select="Valor*1.2" />   /> -->



									
							<!-- 	</tr>
							</table>
							<xsl:choose> -->
								<!-- case of only one page-->
								<!-- <xsl:when test="(position() mod 40 = 0 and position() = 40)"> -->
									<!--40 rows per page-->
									<!-- <xsl:call-template name="Filler">
										<xsl:with-param name="fillercount" select="1" />
									</xsl:call-template>

									<xsl:copy-of select="$ReportFooter" />

									<br class="pagebreak" />

									<xsl:copy-of select="$ReportHeader" />

									<xsl:call-template name="Filler">
										<xsl:with-param name="fillercount" select="2" />
									</xsl:call-template> -->

								<!-- 	<xsl:copy-of select="$EdoResConHeader"/> -->
									
								<!-- </xsl:when> -->
								<!-- case of more than one page-->

								<!--46 rows per page-->

								<!-- <xsl:otherwise>
									<xsl:if test="(40 - position() mod 46) = 0 and position() &gt; (40 + position() mod 40)">
										
										<xsl:call-template name="Filler">
											<xsl:with-param name="fillercount" select="1" />
										</xsl:call-template>

										<xsl:copy-of select="$ReportFooter" />

										<br class="pagebreak" />

										<xsl:copy-of select="$ReportHeader" />

										<xsl:call-template name="Filler">
											<xsl:with-param name="fillercount" select="2" />
										</xsl:call-template>
 -->
										<!-- <xsl:copy-of select="$EdoResConHeader"/> -->

									<!-- </xsl:if>

								</xsl:otherwise>
							</xsl:choose>
						</xsl:for-each> -->

					<!--Filler -->
					<xsl:choose>
						<!-- case of only one page-->
						<xsl:when test="count(Cliente/ClienteDetalles/ClienteDetalle) &lt;= 26">
							<xsl:call-template name="Filler">
								<xsl:with-param name="fillercount" select="26 - (count(Cliente/ClienteDetalles/ClienteDetalle))"/>
							</xsl:call-template>
						</xsl:when>
						<!-- case of more than one page-->
						<xsl:otherwise>
							<xsl:call-template name="Filler">
								<!--(Rows per page = 46) -  (Rows in current page) - (ClienteTotals section rows = 1 ) - (Filler = 1) - (Page Footer = 1) -->
								<xsl:with-param name="fillercount" select="46 - ((count(Cliente/ClienteDetalles/ClienteDetalle) - 40 ) mod 46) - 3 - 1 - 1"/>
							</xsl:call-template>
						</xsl:otherwise>
					</xsl:choose>
					<!--End Filler -->

					<!-- <xsl:copy-of select="$ClienteTotals"/> -->


					<xsl:copy-of select="$ReportFooter"/>

				</body>
			</html>


		</xsl:template>


	<!-- variable ReportHeader-->
	<xsl:variable name="ReportHeader">
		<table class="tableReportHeader" cellspacing="0">
			<tr>
				<td>
					<!-- <img class="imglogo" src="logokey.png" />  Reporte de comisiones para representante de ventas -->
				</td>
				<td align="center">
					<h3 style="color:darkblue; font-family: Arial"><xsl:value-of select="//Parametros/Titulo" /></h3>
					<h3 style="color:darkblue; font-family: Arial;"><xsl:value-of select="//Parametros/Periodo" /></h3>
				</td>
			</tr>
		</table>
		<table class="tableReportHeader" cellspacing="0">
									<tr>
										 
										<td class="documentheader"  align="right">
											CDI:
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
										<td>
											<xsl:value-of select="//Parametros/CDI" />
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
										</tr>
										<tr>
										<td class="documentheader" align="right">
											RIK:
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
										<td>
											<xsl:value-of select="//Parametros/RIK" />
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
										 
									</tr>
 
		</table>

	</xsl:variable>

	<!-- variable ClienteRecipient-->
	

	<!-- variable ClienteHeader-->

	<!-- variable ReportFooter-->
	<xsl:variable name="ReportFooter">
		<table class="tableReportFooter">
			<tr>
				<td style="width:20px;"></td>
				<td>
					<table>
						<tr>
							<td style="font-size: 5pt; text-align: justify;border-top: solid DarkBlue 1px;">
								Versión del Reporte 1.0
							</td>
						</tr>
					</table>
				</td>
				<td style="width:20px;"></td>
			</tr>
		</table>
	</xsl:variable>

	<!-- Template Filler-->
	<xsl:template name="Filler">
		<xsl:param name="fillercount" select="1"/>
		<xsl:if test="$fillercount > 0">
			<table class="tabledetails">
				<tr>
					<td>
						<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
					</td>
				</tr>
			</table>
			<xsl:call-template name="Filler">
				<xsl:with-param name="fillercount" select="$fillercount - 1"/>
			</xsl:call-template>
		</xsl:if>
	</xsl:template>

	<!--variable ClienteDetallesHeader-->
	<xsl:variable name="ClienteDetallesHeader">
		<table class="tabledetails" cellspacing="0" style="table-layout:fixed">
			<tr>
				<td class="tdmargin" />
				<th style="width:40px">
					Factura
				</th>
				<th style="width:60px">
					Fecha Venc.
				</th>
				<th style="width:60px">
					Fecha-Pago
				</th>
				<th style="width:30px">
					Días
				</th>
				<th style="width:50px">
					Importe
				</th>
				<th style="width:50px">
					Utilidad Prima
				</th>
				<th style="width:60px">
					Multiplicador De ajuste por Cobranza
				</th>
				<th style="width:50px">
					Ajuste por Cobranza
				</th>
				<td class="tdmargin" />
			</tr>
		</table>
	</xsl:variable>

	<!--variable ClienteTotals-->
	<xsl:variable name="ClienteTotals">
		<table class="tabledetails" cellspacing="0" style="table-layout:fixed">
			<tr>
				<td class="tdtotalmargin" />
				<td class="documenttotal" align="right">
					Subtotal:
				</td>
				<td  align="right">
					<xsl:value-of select="/Data/Cliente/SubTotal" />
					<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
				</td>
				<td class="tdmargin" />
			</tr>
			<tr>
				<td class="tdtotalmargin" />
				<td class="documenttotal" align="right">
					Freight:
				</td>
				<td  align="right">
					<xsl:value-of select="/Data/Cliente/Freight" />
					<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
				</td>
				<td class="tdmargin" />
			</tr>
			<tr>
				<td class="tdtotalmargin" />
				<td class="documenttotal" align="right">
					Total:
				</td>
				<td   align="right">
					<xsl:value-of select="/Data/Cliente/Total" />
					<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
				</td>
				<td class="tdmargin" />
			</tr>
		</table>
	</xsl:variable>

	 


<!--variable EdoResConHeader-->
	

	<!--variable PlanEspecialHeader-->
	

 
</xsl:stylesheet>
