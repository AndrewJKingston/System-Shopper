SET IDENTITY_INSERT [dbo].[Discount] ON 
GO
INSERT [dbo].[Discount] ([DiscountID], [DiscountName], [DiscountPercent]) VALUES (1, N'Military', CAST(25.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Discount] ([DiscountID], [DiscountName], [DiscountPercent]) VALUES (2, N'Student', CAST(15.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Discount] ([DiscountID], [DiscountName], [DiscountPercent]) VALUES (3, N'None', CAST(0.00 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[Discount] OFF
GO
SET IDENTITY_INSERT [dbo].[Manufacturer] ON 
GO
INSERT [dbo].[Manufacturer] ([ManufacturerID], [ManufacturerName], [ManufacturerBio], [ManufacturerLogo]) VALUES (3, N'NVIDIA', N'NVIDIA sells graphics processing units (GPUs) for gaming, professional visualization, data center, and AI/ML applications, as well as related hardware and software products.', N'https://www.nvidia.com/content/dam/en-zz/Solutions/about-nvidia/logo-and-brand/01-nvidia-logo-horiz-500x200-2c50-p@2x.png')
GO
INSERT [dbo].[Manufacturer] ([ManufacturerID], [ManufacturerName], [ManufacturerBio], [ManufacturerLogo]) VALUES (4, N'AMD', N'AMD sells computer processors (CPUs and GPUs) for gaming, professional visualization, data center, and AI/ML applications, as well as other hardware and software products.', N'https://upload.wikimedia.org/wikipedia/commons/thumb/7/7c/AMD_Logo.svg/2560px-AMD_Logo.svg.png')
GO
INSERT [dbo].[Manufacturer] ([ManufacturerID], [ManufacturerName], [ManufacturerBio], [ManufacturerLogo]) VALUES (5, N'Intel', N'Intel sells computer processors (CPUs and GPUs), motherboards, chipsets, SSDs, NICs, and related software products.', N'https://1000logos.net/wp-content/uploads/2017/02/Intel-Logo-2005.png')
GO
INSERT [dbo].[Manufacturer] ([ManufacturerID], [ManufacturerName], [ManufacturerBio], [ManufacturerLogo]) VALUES (1002, N'Seagate', N'Seagate sells storage solutions, such as HDDs, SSDs, NAS devices, and cloud storage services, as well as data recovery and management software products.', N'https://branding.seagate.com/content/thumb/png/6ae62bb9-4ca7-4b7c-a43c-5e2dac5413ba')
GO
INSERT [dbo].[Manufacturer] ([ManufacturerID], [ManufacturerName], [ManufacturerBio], [ManufacturerLogo]) VALUES (2002, N'Corsair', N'Corsair sells computer hardware, gaming peripherals, and accessories, including keyboards, mice, headsets, power supplies, RAM, cooling systems, SSDs, gaming chairs, PC cases, and pre-built gaming desktops.', N'https://cwsmgmt.corsair.com/press/CORSAIRLogo2020_stack_K.png')
GO
INSERT [dbo].[Manufacturer] ([ManufacturerID], [ManufacturerName], [ManufacturerBio], [ManufacturerLogo]) VALUES (2003, N'MSI', N'MSI sells gaming laptops, desktops, motherboards, graphics cards, all-in-one PCs, gaming monitors, peripherals, and PC components like power supplies, cooling systems, and SSDs.', N'https://logos-world.net/wp-content/uploads/2020/11/MSI-Logo.png')
GO
INSERT [dbo].[Manufacturer] ([ManufacturerID], [ManufacturerName], [ManufacturerBio], [ManufacturerLogo]) VALUES (3002, N'Kingston', N'Kingston sells USB flash drives, memory cards, SSDs, memory modules, encrypted USB drives, and enterprise-level storage solutions for data center and server storage systems.', N'https://1000logos.net/wp-content/uploads/2020/05/Kingston-logo.jpg')
GO
SET IDENTITY_INSERT [dbo].[Manufacturer] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 
GO
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [ManufacturerID], [Price], [DiscountID], [ProductImage]) VALUES (2, N'MSI X570S MAG TOMAHAWK MAX WiFi AMD AM4 ATX Motherboard 4.6 (34)', N'The MSI X570S MAG TOMAHAWK MAX WiFi is an ATX motherboard for AMD AM4 processors with PCIe 4.0, built-in WiFi 6E, Bluetooth 5.2, 2.5G LAN, 4 DIMM slots for up to 128GB DDR4 RAM, M.2 slots for NVMe SSDs, USB 3.2 Gen 2 and Gen 1 ports, and support for multiple GPUs in SLI or Crossfire configurations.', 2003, CAST(229.99 AS Decimal(18, 2)), 1, N'https://90a1c75758623581b3f8-5c119c3de181c9857fcb2784776b17ef.ssl.cf2.rackcdn.com/640444_310110_01_front_comping.jpg')
GO
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [ManufacturerID], [Price], [DiscountID], [ProductImage]) VALUES (1002, N'Kingston 16GB DDR4-2666 PC4-21300 CL19 Single Channel ECC Server Memory Module KTD-PE426E/16G - Green', N'The Kingston 16GB DDR4-2666 PC4-21300 CL19 Single Channel ECC Server Memory Module is a high-performance memory module designed for servers and workstations, featuring a 16GB capacity, 2666MHz speed, ECC support, and a green PCB.', 3002, CAST(59.99 AS Decimal(18, 2)), 1, N'https://90a1c75758623581b3f8-5c119c3de181c9857fcb2784776b17ef.ssl.cf2.rackcdn.com/662859_538496_01_front_comping.jpg')
GO
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [ManufacturerID], [Price], [DiscountID], [ProductImage]) VALUES (1003, N'ASUS NVIDIA GeForce RTX 4090 ROG Strix Overclocked Triple Fan 24 GB GDDR6X PCIe 4.0 Graphics Card', N'The ASUS NVIDIA GeForce RTX 4090 ROG Strix is a high-end graphics card for gaming and professional applications, featuring an overclocked GPU with 10496 CUDA cores, 24GB GDDR6X memory, triple fans for cooling, PCIe 4.0, HDMI 2.1 and DisplayPort 1.4a outputs, and support for up to four displays. It''s compatible with ASUS Aura Sync RGB lighting for customizable visual effects.
', 3, CAST(1999.99 AS Decimal(18, 2)), 1, N'https://90a1c75758623581b3f8-5c119c3de181c9857fcb2784776b17ef.ssl.cf2.rackcdn.com/654069_504944_01_front_comping.jpg')
GO
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [ManufacturerID], [Price], [DiscountID], [ProductImage]) VALUES (2002, N'MSI B550 MAG Tomahawk AMD AM4 ATX Motherboard', N'The MSI B550 MAG Tomahawk is a powerful ATX motherboard that supports AMD Ryzen processors, PCIe 4.0, DDR4 memory up to 5100 MHz (OC), and has dual M.2 slots, USB 3.2 Gen 2 Type-C and Type-A ports, onboard WiFi 6, and RGB lighting.', 2003, CAST(169.99 AS Decimal(18, 2)), 1, N'https://90a1c75758623581b3f8-5c119c3de181c9857fcb2784776b17ef.ssl.cf2.rackcdn.com/625047_130906_01_front_comping.jpg')
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
