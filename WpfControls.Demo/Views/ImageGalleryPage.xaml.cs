﻿// Tom4u.Toolkit
// Copyright (C) 2020  Thomas Ohms
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using Tom4u.Toolkit.WpfControls.ImageGallery;

namespace WpfControls.Demo.Views
{
    public partial class ImageGalleryPage
    {
        public ImageGalleryPage()
        {
            InitializeComponent();
            ImageGalleryView gallery = new ImageGalleryView(GetSimulatedViewModel());
            gallery.ImageClicked += ImageGalleryView_OnImageClicked;
            gallery.GalleryClosed += ImageGalleryView_OnGalleryClosed;
            MainGrid.Children.Add(gallery);
        }

        private void ImageGalleryView_OnImageClicked(object sender, ImageViewModel e)
        {
            MessageBox.Show($"{e.Title} clicked");
        }

        private void ImageGalleryView_OnGalleryClosed(object sender, EventArgs e)
        {
            if (NavigationService == null || !NavigationService.CanGoBack)
            {
                return;
            }

            NavigationService?.GoBack();
        }

        private static ImageGalleryViewModel GetSimulatedViewModel()
        {
            ImageGalleryViewModel viewModel = new ImageGalleryViewModel();

            for (int i = 1; i < 3; i++)
            {
                viewModel.Categories.Add(GetSimulatedCategory($"Category {i}", viewModel.CurrentThumbnailSize));
            }

            return viewModel;
        }

        private static ImagesCategoryViewModel GetSimulatedCategory(string categoryName, int defaultImageSize)
        {
            ImagesCategoryViewModel viewModel = new ImagesCategoryViewModel { CategoryName = categoryName };
            List<ImageViewModel> images = new List<ImageViewModel>();

            for (int i = 1; i < 10; i++)
            {
                ImageViewModel image = GetSimulatedImage(defaultImageSize, $"Image{i}");
                images.Add(image);
            }

            foreach (ImageViewModel image in images)
            {
                viewModel.Images.Add(image);
            }

            return viewModel;
        }

        private static ImageViewModel GetSimulatedImage(int defaultSize, string imageTitle)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string path = Path.GetDirectoryName(assembly.CodeBase) ?? string.Empty;
            string filePath = Path.Combine(path, "Images", "DummyImage.png");
            ImageViewModel viewModel = new ImageViewModel
            {
                Title = imageTitle,
                ImageSize = defaultSize,
                Path = filePath,
                Tags = ""
            };

            return viewModel;
        }
    }
}