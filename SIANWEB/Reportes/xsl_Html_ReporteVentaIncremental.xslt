<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:js="urn:extra-functions">
	<xsl:output method="html" indent="yes"/>
    <xsl:template match="Data">
			<html>
				<head>
					<title>Reporte de venta incremental</title>
					<meta http-equiv="content-type" content="text/html; charset=UTF-8"/>
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
					</style>
				</head>
				
				<body>

					<xsl:copy-of select="$ReportHeader"/>


					<!-- PONGO PRIMERO EL RESUMEN -->

						<br></br>

						<table class="tabledetails" cellspacing="0" style="table-layout:fixed">
								<tr>
									 <th class="tdmargin" style="border: solid 0px " />
									<th style="width:100px;border: solid 0px "   ></th>

									<th style="width:150px">
										Vta. Ene 2016
									</th> 
									<th style="width:150px">
										 Vta. Feb 2016
									</th>
									<th style="width:150px">
										 Vta. Mar 2016
									</th>
									<th style="width:150px">
										 Total Promedio
									</th>

								 
								</tr>
				 
						 
								<tr style="font-family:Arial; font-size:10pt; color:black;  text-align:center;border: solid 1px darkblue;">
									<td class="tdmargin" />
									<td style="width:100px;border: solid 1px darkblue;font-family:Arial; font-size:10pt; color:Black; font-weight:bold;" align="left" >Venta Inst. Incremental Acumulada:</td>
									 	
									<td style="width:150px;border: solid 1px darkblue;" align="right" >
										<xsl:value-of select="concat('$ ', format-number(Resumen/VtaInstIncrementalMes1,  '###,##0.00'))" />
										<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
									</td>
									 
									<td style="width:150px;border: solid 1px darkblue;" align="right" >
										<xsl:value-of select="concat('$ ', format-number(Resumen/VtaInstIncrementalMes2,  '###,##0.00'))" />
										<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
									</td>
									<td style="width:150px;border: solid 1px darkblue;" align="right" >
										<xsl:value-of select="concat('$ ', format-number(Resumen/VtaInstIncrementalMes3,  '###,##0.00'))" />
										<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
									</td>
									<td style="width:150px;border: solid 1px darkblue;" align="right" >
										<xsl:value-of select="concat('$ ', format-number(Resumen/VtaInstIncrementalTPromedio,  '###,##0.00'))" />
										<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
									</td>
									 
									<td class="tdmargin" />

									
								</tr>
								<tr>
										<td class="tdmargin" />
										<td style="width:100px;border: solid 1px darkblue;font-family:Arial; font-size:10pt; color:Black; font-weight:bold;" align="left" >Objetivo Mensual:</td>
										 	
										<td style="width:150px;border: solid 1px darkblue;" align="right" >
											<xsl:value-of select="concat('$ ', format-number(Resumen/ObjetivoMes1,  '###,##0.00'))" />
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
										 
										<td style="width:150px;border: solid 1px darkblue;" align="right" >
											<xsl:value-of select="concat('$ ', format-number(Resumen/ObjetivoMes2,  '###,##0.00'))" />
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
										<td style="width:150px;border: solid 1px darkblue;" align="right" >
											<xsl:value-of select="concat('$ ', format-number(Resumen/ObjetivoMes3,  '###,##0.00'))" />
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
										<td style="width:150px;border: solid 1px darkblue;" align="right" >
											<xsl:value-of select="concat('$ ', format-number(Resumen/ObjetivoTotal,  '###,##0.00'))" /> 
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
										 
										<td class="tdmargin" />
								</tr>
								
									<tr>
										<td class="tdmargin" />
										<td style="width:100px;border: solid 1px darkblue;font-family:Arial; font-size:10pt; color:Black; font-weight:bold;" align="left" >Objetivo Acumulado:</td>
										 	
										<td style="width:150px;border: solid 1px darkblue;" align="right" >
											<xsl:value-of select="concat('$ ', format-number(Resumen/ObjetivoAcumuladoMes1,  '###,##0.00'))" />
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
										 
										<td style="width:150px;border: solid 1px darkblue;" align="right" >
											<xsl:value-of select="concat('$ ', format-number(Resumen/ObjetivoAcumuladoMes2,  '###,##0.00'))" />
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
										<td style="width:150px;border: solid 1px darkblue;" align="right" >
											<xsl:value-of select="concat('$ ', format-number(Resumen/ObjetivoAcumuladoMes3,  '###,##0.00'))" />
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
										<td style="width:150px;border: solid 1px darkblue;" align="right" >
											<xsl:value-of select="concat('$ ', format-number(Resumen/ObjetivoAcumuladoTotal,  '###,##0.00'))" /> 
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
										 
										<td class="tdmargin" />

									
								</tr>
								


							</table>
						

						<br></br>
						<table class="tabledetails" cellspacing="0" style="table-layout:fixed">
								<tr>
										<td class="tdmargin" />
										<td class="negrita" style="width:150px" align="center" >% Cumplimiento Venta Incrementa:</td>
										 	
										
										<td class="negrita" style="width:60px; color:Red; " align="left" >
											<xsl:value-of select="concat('$ ', format-number(Resumen/PorcVtaIncrementa,  '###,##0.00'))" />
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
										<td class="negrita"  style="width:100px;" align="left" >
											% Multiplicador
										</td>
										<td class="negrita"  style="width:120px;color:Red; " align="left" >
											<xsl:value-of select="concat('$ ', format-number(Resumen/PorcMultiplicador,  '###,##0.00'))" /> 
											<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
										</td>
										<td style="width:50px" align="center" ></td>

										 
										<td class="tdmargin" />

									
								</tr>
							</table>


						<br></br>



					<xsl:for-each select="Cliente">

						<xsl:call-template name="Filler">
							<xsl:with-param name="fillercount" select="1" />
						</xsl:call-template>

						<!-- <xsl:copy-of select="$ClienteRecipient"/> -->

						<xsl:call-template name="Filler">
							<xsl:with-param name="fillercount" select="1" />
						</xsl:call-template>

						<!-- <xsl:copy-of select="$ClienteHeader"/> -->

						<!--  <table class="tableReportHeader" cellspacing="0"> -->
						<table  cellspacing="0">
															<tr>
																 
																<td class="documentheader"  align="left">
																	Cliente :
																	<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
																</td>
																<td class="negrita" >
																	<xsl:value-of select="Cte_NomComercial" />
																	<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
																	 
																</td>
																 
							 								</tr>
						 
						</table>


						<xsl:call-template name="Filler">
							<xsl:with-param name="fillercount" select="1" />
						</xsl:call-template>
 


				<!-- Inicio de el each de GrupoVtasInstaladas -->

							<xsl:for-each select="GrupoVtasInstaladas/GrupoVtaInstalada">

								<table class="tabledetails" cellspacing="0">
											<tr>
												 
												<td class="documentheader"  align="left">
													<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
												</td>
												<td class="negrita" >
													<xsl:value-of select="NombreGrupoVtaInstalada" />
													<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
													 
												</td>
												 
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

									<br class="pagebreak" />

									<xsl:copy-of select="$ReportHeader" />

									<xsl:call-template name="Filler">
										<xsl:with-param name="fillercount" select="2" />
									</xsl:call-template>

									<xsl:copy-of select="$ClienteDetallesHeader"/>
									
								</xsl:when>
								<!-- case of more than one page-->
								<xsl:otherwise>
									<xsl:if test="(40 - position() mod 46) = 0 and position() &gt; (40 + position() mod 40)">
										<!--46 rows per page-->
										<xsl:call-template name="Filler">
											<xsl:with-param name="fillercount" select="1" />
										</xsl:call-template>

										<xsl:copy-of select="$ReportFooter" />

										<br class="pagebreak" />

										<xsl:copy-of select="$ReportHeader" />

										<xsl:call-template name="Filler">
											<xsl:with-param name="fillercount" select="2" />
										</xsl:call-template>

										<xsl:copy-of select="$ClienteDetallesHeader"/>

									</xsl:if>

								</xsl:otherwise>
							</xsl:choose>


						<!-- fin de el each de GrupoVtasInstaladas -->


									<!-- Inicio de el each de Categoria -->

										<xsl:for-each select="Categorias/Categoria">

											<table class="tabledetails" cellspacing="0">
														<tr>
															 
															<td class="documentheader"  align="left">
																<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
															</td>
															<td class="negrita" >
																<xsl:value-of select="NombreCategoria" />
																<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
																 
															</td>
															 
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

												<br class="pagebreak" />

												<xsl:copy-of select="$ReportHeader" />

												<xsl:call-template name="Filler">
													<xsl:with-param name="fillercount" select="2" />
												</xsl:call-template>

												<xsl:copy-of select="$ClienteDetallesHeader"/>
												
											</xsl:when>
											<!-- case of more than one page-->
											<xsl:otherwise>
												<xsl:if test="(40 - position() mod 46) = 0 and position() &gt; (40 + position() mod 40)">
													<!--46 rows per page-->
													<xsl:call-template name="Filler">
														<xsl:with-param name="fillercount" select="1" />
													</xsl:call-template>

													<xsl:copy-of select="$ReportFooter" />

													<br class="pagebreak" />

													<xsl:copy-of select="$ReportHeader" />

													<xsl:call-template name="Filler">
														<xsl:with-param name="fillercount" select="2" />
													</xsl:call-template>

													<xsl:copy-of select="$ClienteDetallesHeader"/>

												</xsl:if>

											</xsl:otherwise>
										</xsl:choose>


									<!-- fin de el each de Categoria -->




									<xsl:copy-of select="$ClienteDetallesHeader"/>

			<!-- EJEMPLO DE COMO PONER FORMATO DE PORCENTAJE 
			                    	<td style="width:50px" align="right" class="blueline">
													<xsl:value-of select="concat(UP, ' %')" />
													<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
												</td> -->

									<xsl:for-each select="Productos/Producto">

										<table class="tabledetails" cellspacing="0" style="table-layout:fixed">
											<tr>
												<td class="tdmargin" />
												<td style="width:40px" align="center"  >
													<xsl:value-of select="Id_Prd" />
													<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
												</td>
												<td   style="width:120px" align="left"  >
													<xsl:value-of select="Prd_Descripcion" />
													<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
												</td>
												<td style="width:30px" align="right" >
													<xsl:value-of select="Prd_Presentacion" />
													<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
												</td>
												<td style="width:50px" align="right"  >
													<xsl:value-of select="concat('$ ', format-number(Est_IFeb,  '###,##0.00'))" />
													<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
												</td>
												<td style="width:50px" align="right"  >
													<xsl:value-of select="concat('$ ',format-number(Est_IMar,  '###,##0.00'))" />
													<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
												</td>
												<td style="width:60px" align="right"  >
													<xsl:value-of select=" format-number(Est_IAbr,  '###,##0.00')" />
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

													<br class="pagebreak" />

													<xsl:copy-of select="$ReportHeader" />

													<xsl:call-template name="Filler">
														<xsl:with-param name="fillercount" select="2" />
													</xsl:call-template>

													<xsl:copy-of select="$ClienteDetallesHeader"/>
													
												</xsl:when>
												<!-- case of more than one page-->
												<xsl:otherwise>
													<xsl:if test="(40 - position() mod 46) = 0 and position() &gt; (40 + position() mod 40)">
														<!--46 rows per page-->
														<xsl:call-template name="Filler">
															<xsl:with-param name="fillercount" select="1" />
														</xsl:call-template>

														<xsl:copy-of select="$ReportFooter" />

														<br class="pagebreak" />

														<xsl:copy-of select="$ReportHeader" />

														<xsl:call-template name="Filler">
															<xsl:with-param name="fillercount" select="2" />
														</xsl:call-template>

														<xsl:copy-of select="$ClienteDetallesHeader"/>

													</xsl:if>

												</xsl:otherwise>
										</xsl:choose>
									</xsl:for-each>


								<!-- <xsl:copy-of select="$CategoriaTotales"/>  -->
								<!-- totales del grupo por cliente   -->

								<table class="tabledetails" cellspacing="0" style="table-layout:fixed">
									<tr>
										<td class="tdmargin" />
										<td class="negrita"  style="width:40px" align="center"  >
											
										</td>
										<td   class="negrita"  style="width:120px" align="left"  >
										
										</td>
										
										<td class="negrita"  style="width:30px" align="right" >
											
										</td>
										<td  class="negrita"  style="width:50px" align="right"  >
										___________
										</td>
										<td  class="negrita"  style="width:50px" align="right"  >
										___________
										</td>
										<td class="negrita"  style="width:60px" align="right"  >
										___________
										</td>
										<td class="tdmargin" />

									 
									</tr>


											<tr>
												<td class="tdmargin" />
												<td style="width:40px" align="center"  >
													 
												</td>
												<td   class="negrita"  style="width:120px" align="left"  >
													Total de 
													<xsl:value-of select="NombreCategoria" />
													<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
												</td>
												<td class="negrita"  style="width:30px" align="right" >
													 
												</td>
												<td class="negrita"  style="width:50px" align="right"  >
												 	<xsl:value-of select="concat('$ ',format-number(
												   sum(Productos/Producto/Est_IFeb),  '###,##0.00'))" /> <!-- <xsl:value-of select="concat('$ ', Importe)" /> -->
													<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
												</td>
												<td class="negrita"  style="width:50px" align="right"  >
													<xsl:value-of select="concat('$ ',format-number(sum(Productos/Producto/Est_IMar),  '###,##0.00'))" />
													<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
												</td>
												<td class="negrita"  style="width:60px" align="right"  >
													<xsl:value-of select="concat('$ ',format-number(
												   sum(Productos/Producto/Est_IAbr),  '###,##0.00'))" />
												</td>
												<td class="tdmargin" />
									
												
											</tr>
										</table>
 									
							</xsl:for-each> <!--Categoria -->
					 

			<!-- 	<xsl:copy-of select="$GrupoTotales"/>  -->
			<!-- <xsl:copy-of select="$CategoriaTotales"/>  -->
								<!-- totales del grupo por cliente   -->

								<table class="tabledetails" cellspacing="0" style="table-layout:fixed">
									<tr>
										<td class="tdmargin" />
										<td class="negrita"  style="width:40px" align="center"  >
											
										</td>
										<td   class="negrita"  style="width:120px" align="left"  >
										
										</td>
										
										<td class="negrita"  style="width:30px" align="right" >
											
										</td>
										<td  class="negrita"  style="width:50px" align="right"  >
										___________
										</td>
										<td  class="negrita"  style="width:50px" align="right"  >
										___________
										</td>
										<td class="negrita"  style="width:60px" align="right"  >
										___________
										</td>
										<td class="tdmargin" />

									 
									</tr>


											<tr>
												<td class="tdmargin" />
												<td style="width:40px" align="center"  >
													 
												</td>
												<td   class="negrita"  style="width:120px" align="left"  >
													Total de 
													<xsl:value-of select="NombreGrupoVtaInstalada" />
													<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
												</td>
												<td class="negrita"  style="width:30px" align="right" >
													 
												</td>
												<td class="negrita"  style="width:50px" align="right"  >
												 	<xsl:value-of select="concat('$ ',format-number(
												   sum(Categorias/Categoria/Productos/Producto/Est_IFeb),  '###,##0.00'))" /> <!-- <xsl:value-of select="concat('$ ', Importe)" /> -->
													<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
												</td>
												<td class="negrita"  style="width:50px" align="right"  >
													<xsl:value-of select="concat('$ ',format-number(sum(Categorias/Categoria/Productos/Producto/Est_IMar),  '###,##0.00'))" />
													<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
												</td>
												<td class="negrita"  style="width:60px" align="right"  >
													<xsl:value-of select="concat('$ ',format-number(
												   sum(Categorias/Categoria/Productos/Producto/Est_IAbr),  '###,##0.00'))" />
												</td>
												<td class="tdmargin" />
									
												
											</tr>
										</table>
							<br></br>

				</xsl:for-each> <!--grupo -->
 
								<!-- totales  por cliente  -->

								<table class="tabledetails" cellspacing="0" style="table-layout:fixed">
									<tr>
										<td class="tdmargin" />
										<td class="negrita"  style="width:40px" align="center"  >
											
										</td>
										<td   class="negrita"  style="width:120px" align="left"  >
										
										</td>
										
										<td class="negrita"  style="width:30px" align="right" >
											
										</td>
										<td  class="negrita"  style="width:50px" align="right"  >
										___________
										</td>
										<td  class="negrita"  style="width:50px" align="right"  >
										___________
										</td>
										<td class="negrita"  style="width:60px" align="right"  >
										___________
										</td>
										<td class="tdmargin" />

									 
									</tr>


											<tr>
												<td class="tdmargin" />
												<td style="width:40px" align="center"  >
													 
												</td>
												<td   class="negrita"  style="width:120px" align="left"  >
													Total de Cliente
												</td>
												<td class="negrita"  style="width:30px" align="right" >
													 
												</td>
												<td class="negrita"  style="width:50px" align="right"  >
												 	<xsl:value-of select="concat('$ ',format-number(
												   sum(GrupoVtasInstaladas/GrupoVtaInstalada/Categorias/Categoria/Productos/Producto/Est_IFeb),  '###,##0.00'))" /> <!-- <xsl:value-of select="concat('$ ', Importe)" /> -->
													<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
												</td>
												<td class="negrita"  style="width:50px" align="right"  >
													<xsl:value-of select="concat('$ ',format-number(sum(GrupoVtasInstaladas/GrupoVtaInstalada/Categorias/Categoria/Productos/Producto/Est_IMar),  '###,##0.00'))" />
													<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
												</td>
												<td class="negrita"  style="width:60px" align="right"  >
													<xsl:value-of select="concat('$ ',format-number(
												   sum(Categorias/GrupoVtasInstaladas/GrupoVtaInstalada/Categoria/Productos/Producto/Est_IAbr),  '###,##0.00'))" />
												</td>
												<td class="tdmargin" />
									
												
											</tr>
										</table>
										<br></br>
	
						</xsl:for-each>		<!-- Cliente  --> 



			<br></br>

				 	<!-- case of only one page-->
<!-- case of more than one page-->
<!--(Rows per page = 46) -  (Rows in current page) - (ClienteTotals section rows = 1 ) - (Filler = 1) - (Page Footer = 1) -->
					<!--Filler -->
					<xsl:choose>
					
						<xsl:when test="count(Cliente/GrupoVtasInstaladas/GrupoVtaInstalada/Categorias/Categoria/Productos/Producto) &lt;= 40">
							<xsl:call-template name="Filler">
								<xsl:with-param name="fillercount" select="40 - (count(Cliente/GrupoVtasInstaladas/GrupoVtaInstalada/Categorias/Categoria/Productos/Producto))"/>
							</xsl:call-template>
						</xsl:when>
						
						<xsl:otherwise>
							<xsl:call-template name="Filler">
								
								<xsl:with-param name="fillercount" select="46 - ((count(Cliente/GrupoVtasInstaladas/GrupoVtaInstalada/Categorias/Categoria/Productos/Producto) - 40 ) mod 46) - 3 - 1 - 1"/>
							</xsl:call-template>
						</xsl:otherwise>
					</xsl:choose>
					<!--End Filler -->
 
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
							<h3 style="color:darkblue; font-family: Arial"><xsl:value-of select="//Parametros/Cd_Nombre" /></h3>
							<h3 style="color:darkblue; font-family: Arial"><xsl:value-of select="//Parametros/Mes_Nombre"/> <xsl:value-of select="//Parametros/Anio_Nombre"/> </h3>
					 
							<h3 style="color:darkblue; font-family: Arial;"><xsl:value-of select="//Parametros/Rik_Nombre" /></h3>
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
													<xsl:value-of select="//Parametros/Cd_Nombre" />
													<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
												</td>
												</tr>
												<tr>
												<td class="documentheader" align="right">
													RIK:
													<xsl:value-of select="translate(' ', ' ', '&#160;')"/>
												</td>
												<td>
													<xsl:value-of select="//Parametros/Rik_Nombre" />
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
								Reporte obtenido con XML y XSLT
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
					Clave
				</th>
				<th style="width:120px">
					Descripción
				</th>
				<th style="width:30px">
					Present.
				</th>
				<th style="width:50px">
					Vta. Ene 2016 $
				</th>
				<th style="width:50px">
					Vta. Feb 2016 $
				</th>
				<th style="width:60px">
					Vta. Mar 2016 $
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
